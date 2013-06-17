using System;
using Newtonsoft.Json;

namespace Sketch.Web.Serialization
{
    public class TimeSpanJsonConverter: JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var timeSpan = (TimeSpan) value;
            serializer.Serialize(writer, timeSpan.TotalMilliseconds);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (TimeSpan);
        }
    }
}