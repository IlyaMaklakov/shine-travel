﻿#region Usings

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

#endregion

namespace Shine.Framework.Json
{
    public static class JsonExtensions
    {
        /// <summary>
        ///     Converts given object to JSON string.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            options.Converters.Insert(0, new ShineDateTimeConverter());

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}