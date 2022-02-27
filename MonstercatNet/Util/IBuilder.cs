namespace SoftThorn.MonstercatNet
{
    public interface IBuilder<out TElement>
    {
        TElement Build();
    }
}
