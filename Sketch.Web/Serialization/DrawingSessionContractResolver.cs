using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sketch.Web.Serialization
{
    public class DrawingSessionContractResolver: CamelCasePropertyNamesContractResolver
    {
        public DrawingSessionContractResolver()
        {
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            var photosProperty = properties.Single(x => x.UnderlyingName == "DrawingSession");
            properties.Remove(photosProperty);
            return properties;
        }

    }
}