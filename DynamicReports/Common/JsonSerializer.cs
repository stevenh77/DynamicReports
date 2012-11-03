using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DynamicReports.Common
{
    public class JsonSerializer
    {
        public static T Deserialize<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
