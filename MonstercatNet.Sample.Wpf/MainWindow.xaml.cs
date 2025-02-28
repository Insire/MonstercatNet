using NAudio.Wave;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Threading;
using SoftThorn.MonstercatNet;
using Microsoft.Extensions.Configuration;
using MonstercatNet.Utilities;

namespace MonstercatNet.Sample.Wpf
{
    public partial class MainWindow : Window
    {
        private volatile bool _fullyDownloaded;
        private VolumeWaveProvider16? _volumeProvider;
        private BufferedWaveProvider? _bufferedWaveProvider;
        private IWavePlayer? _waveOut;

        private readonly DispatcherTimer _timer;
        private volatile StreamingPlaybackState _playbackState;
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

            var sectionName = nameof(ApiCredentials);
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
                await using var responseStream = stream;

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
                        Debug.WriteLine("Decompressed a frame {0}", decompressed);
                        _bufferedWaveProvider?.AddSamples(buffer, 0, decompressed);
                    }
                } while (_playbackState != StreamingPlaybackState.Stopped);

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

        private static IWavePlayer CreateWaveOut()
        {
            return new WaveOut();
        }

        private static void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            Debug.WriteLine("Playback Stopped");
            if (e.Exception != null)
            {
                MessageBox.Show($"Playback Error {e.Exception.Message}");
            }
        }

        private void timer1_Tick(object? sender, EventArgs e)
        {
            if (_playbackState == StreamingPlaybackState.Stopped)
            {
                return;
            }

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
                if (bufferedSeconds < 0.5 && _playbackState == StreamingPlaybackState.Playing && !_fullyDownloaded)
                {
                    Pause();
                }
                else if (bufferedSeconds > 4 && _playbackState == StreamingPlaybackState.Buffering)
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

        private void Play()
        {
            _waveOut?.Play();
            Debug.WriteLine("Started playing, waveOut.PlaybackState={0}", _waveOut?.PlaybackState);
            _playbackState = StreamingPlaybackState.Playing;
        }

        private void Pause()
        {
            _playbackState = StreamingPlaybackState.Buffering;
            _waveOut?.Pause();
            Debug.WriteLine("Paused to buffer, waveOut.PlaybackState={0}", _waveOut?.PlaybackState);
        }

        private void StopPlayback()
        {
            if (_playbackState == StreamingPlaybackState.Stopped)
            {
                return;
            }

            _playbackState = StreamingPlaybackState.Stopped;
            if (_waveOut == null)
            {
                return;
            }

            _waveOut.Stop();
            _waveOut.Dispose();
            _waveOut = null;
        }

        private async void buttonPlay_Click(object? sender, RoutedEventArgs e)
        {
            try
            {
                if (_playbackState == StreamingPlaybackState.Stopped)
                {
                    _playbackState = StreamingPlaybackState.Buffering;
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
                else if (_playbackState == StreamingPlaybackState.Paused)
                {
                    _playbackState = StreamingPlaybackState.Buffering;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void Window_Loaded(object? sender, RoutedEventArgs e)
        {
            try
            {
                await _api.Login(Credentials);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
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
