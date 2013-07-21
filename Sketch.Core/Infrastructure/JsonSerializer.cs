using System;
using Newtonsoft.Json;

namespace Sketch.Core.Infrastructure
{
    public class JsonSerializer: ITextSerializer
    {
        public string Serialize<T>(T serializable)
        {
            return JsonConvert.SerializeObject(serializable);
        }

        public T Deserialize<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public object Deserialize(string serialized, Type type)
        {
            return JsonConvert.DeserializeObject(serialized, type);
        }
    }
}