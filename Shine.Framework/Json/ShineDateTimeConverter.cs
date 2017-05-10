#region Usings

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Shine.Framework.Timing;

#endregion

namespace Shine.Framework.Json
{
    public class ShineDateTimeConverter : IsoDateTimeConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
            {
                return true;
            }

            return false;
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var date = base.ReadJson(reader, objectType, existingValue, serializer) as DateTime?;

            if (date.HasValue)
            {
                return Clock.Normalize(date.Value);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = value as DateTime?;
            base.WriteJson(writer, date.HasValue ? Clock.Normalize(date.Value) : value, serializer);
        }
    }
}