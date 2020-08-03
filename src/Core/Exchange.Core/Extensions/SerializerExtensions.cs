using Exchange.Core.Helpers;
using Newtonsoft.Json;

namespace Exchange.Core.Extensions
{
    /// <summary>
    /// Serializer Extension
    /// </summary>
    public static class SerializerExtensions
    {
        /// <summary>
        /// Convert T Object to Json
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToJson<T>(this T self)
            => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}