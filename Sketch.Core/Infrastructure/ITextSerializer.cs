namespace Sketch.Core.Infrastructure
{
    public interface ITextSerializer
    {
        string Serialize<T>(T serializable);
    }
}