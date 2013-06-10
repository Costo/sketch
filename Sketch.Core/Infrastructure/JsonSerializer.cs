using Newtonsoft.Json;

namespace Sketch.Core.Infrastructure
{
    public class JsonSerializer: ITextSerializer
    {
        public string Serialize<T>(T serializable)
        {
            return JsonConvert.SerializeObject(serializable);
        }
    }
}