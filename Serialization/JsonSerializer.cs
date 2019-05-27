using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Serialization
{
    public class JsonSerializer
    {
        public static void Serialize<T>(T source, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path); 
                
            }

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };


            using (StreamWriter file = new StreamWriter(path, true))
            using (JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(file))
            {
                serializer.Serialize(writer, source, typeof(T));
            }
        }

        public static T Deserialize<T>(string path)
        {

            using (StreamReader sr = new StreamReader(path, true))
            {
                T obj = JsonConvert.DeserializeObject<T>(sr.ReadToEnd(), new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                });
                return obj;
            }
           

        }
    }
}
