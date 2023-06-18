using NAudio.Wave;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using SoftThorn.MonstercatNet;
using SoftThorn.MonstercatNet.Tests;
using Microsoft.Extensions.Configuration;

namespace MonstercatNet.Sample.Wpf
{
    public partial class MainWindow : Window
    {
        private volatile bool _fullyDownloaded;
        private VolumeWaveProvider16? _volumeProvider;
        private BufferedWaveProvider? _bufferedWaveProvider;
        private IWavePlayer? _waveOut;

        private readonly DispatcherTimer _timer;
        private volatile StreamingPlaybackState playbackState;
        private readonly HttpClient _httpClient;
        private readonly IMonstercatApi _api;

        protected internal ApiCredentials Credentials { get; } = new ApiCredentials();

        private bool IsBufferNearlyFull => _bufferedWaveProvider != null
            && _bufferedWaveProvider.BufferLength - _bufferedWaveProvider.BufferedBytes < _bufferedWaveProvider.WaveFormat.AverageBytesPerSecond / 4;

        private static IMp3FrameDecompressor CreateFrameDecompressor(Mp3Frame frame)
        {
            WaveFormat waveFormat = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2, frame.FrameLength, frame.BitRate);
            return new AcmMp3FrameDecompressor(waveFormat);
        }

        public MainWindow()
        {
            _httpClient = new HttpClient(new HttpLoggingHandler()).UseMonstercatApiV2();
            _api = MonstercatApi.Create(_httpClient);

            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<MainWindow>()
                .Build();

            var sectionName = typeof(ApiCredentials).Name;
            var section = configuration.GetSection(sectionName);
            section.Bind(Credentials);

            InitializeComponent();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(250)
            };
            _timer.Tick += timer1_Tick;
        }

        private async Task Play(Stream stream)
        {
            IMp3FrameDecompressor? decompressor = null;
            var buffer = new byte[16384 * 4]; // needs to be big enough to hold a decompressed frame

            try
            {
                using var responseStream = stream;

                var readFullyStream = new ReadFullyStream(responseStream);
                do
                {
                    if (IsBufferNearlyFull)
                    {
                        Debug.WriteLine("Buffer getting full, taking a break");
                        await Task.Delay(500);
                    }
                    else
                    {
                        Mp3Frame frame;
                        try
                        {
                            frame = Mp3Frame.LoadFromStream(readFullyStream);
                        }
                        catch (EndOfStreamException)
                        {
                            _fullyDownloaded = true;
                            // reached the end of the MP3 file / stream
                            break;
                        }

                        if (frame == null)
                            break;

                        if (decompressor == null)
                        {
                            // don't think these details matter too much - just help ACM select the right codec
                            // however, the buffered provider doesn't know what sample rate it is working at
                            // until we have a frame
                            decompressor = CreateFrameDecompressor(frame);
                            _bufferedWaveProvider = new BufferedWaveProvider(decompressor.OutputFormat)
                            {
                                BufferDuration = TimeSpan.FromSeconds(20) // allow us to get well ahead of ourselves
                            };
                            //this.bufferedWaveProvider.BufferedDuration = 250;
                        }

                        var decompressed = decompressor.DecompressFrame(frame, buffer, 0);
                        Debug.WriteLine(string.Format("Decompressed a frame {0}", decompressed));
                        _bufferedWaveProvider?.AddSamples(buffer, 0, decompressed);
                    }
                } while (playbackState != StreamingPlaybackState.Stopped);

                Debug.WriteLine("Exiting");
                // was doing this in a finally block, but for some reason
                // we are hanging on response stream .Dispose so never get there
                decompressor?.Dispose();
            }
            finally
            {
                decompressor?.Dispose();
            }
        }

        private IWavePlayer CreateWaveOut()
        {
            return new WaveOut();
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            Debug.WriteLine("Playback Stopped");
            if (e.Exception != null)
            {
                MessageBox.Show(string.Format("Playback Error {0}", e.Exception.Message));
            }
        }

        private void timer1_Tick(object? sender, EventArgs e)
        {
            if (playbackState != StreamingPlaybackState.Stopped)
            {
                if (_waveOut == null && _bufferedWaveProvider != null)
                {
                    Debug.WriteLine("Creating WaveOut Device");

                    _waveOut = CreateWaveOut();
                    _waveOut.PlaybackStopped += OnPlaybackStopped;
                    _volumeProvider = new VolumeWaveProvider16(_bufferedWaveProvider)
                    {
                        Volume = 0.5f
                    };
                    _waveOut.Init(_volumeProvider);
                }
                else if (_bufferedWaveProvider != null)
                {
                    var bufferedSeconds = _bufferedWaveProvider.BufferedDuration.TotalSeconds;

                    // make it stutter less if we buffer up a decent amount before playing
                    if (bufferedSeconds < 0.5 && playbackState == StreamingPlaybackState.Playing && !_fullyDownloaded)
                    {
                        Pause();
                    }
                    else if (bufferedSeconds > 4 && playbackState == StreamingPlaybackState.Buffering)
                    {
                        Play();
                    }
                    else if (_fullyDownloaded && bufferedSeconds == 0)
                    {
                        Debug.WriteLine("Reached end of stream");
                        StopPlayback();
                    }
                }
            }
        }

        private void Play()
        {
            _waveOut?.Play();
            Debug.WriteLine(string.Format("Started playing, waveOut.PlaybackState={0}", _waveOut?.PlaybackState));
            playbackState = StreamingPlaybackState.Playing;
        }

        private void Pause()
        {
            playbackState = StreamingPlaybackState.Buffering;
            _waveOut?.Pause();
            Debug.WriteLine(string.Format("Paused to buffer, waveOut.PlaybackState={0}", _waveOut?.PlaybackState));
        }

        private void StopPlayback()
        {
            if (playbackState != StreamingPlaybackState.Stopped)
            {
                playbackState = StreamingPlaybackState.Stopped;
                if (_waveOut != null)
                {
                    _waveOut.Stop();
                    _waveOut.Dispose();
                    _waveOut = null;
                }
            }
        }

        private async void buttonPlay_Click(object? sender, RoutedEventArgs e)
        {
            if (playbackState == StreamingPlaybackState.Stopped)
            {
                playbackState = StreamingPlaybackState.Buffering;
                _bufferedWaveProvider = null;

                var stream = await _api.StreamTrackAsStream(new TrackStreamRequest()
                {
                    ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                    TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
                });

                var task = Task.Run(() => Play(stream));
                _timer.Start();
                await task;
            }
            else if (playbackState == StreamingPlaybackState.Paused)
            {
                playbackState = StreamingPlaybackState.Buffering;
            }
        }

        private async void Window_Loaded(object? sender, RoutedEventArgs e)
        {
            await _api.Login(Credentials);
        }

        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            ReportGeneratorUtility.Generate();
        }
    }

    public enum StreamingPlaybackState
    {
        Stopped,
        Playing,
        Buffering,
        Paused
    }
}
