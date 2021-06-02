using System.IO;
using Newtonsoft.Json;

namespace YouTubeDownloader.Utils
{
    /// <summary>
    /// Provides methods for serializing and deserializing data in the Json format.
    /// </summary>
    internal static class Json
    {
        /// <summary>
        /// Converts a .NET object to a Json string and writes it to a file.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <param name="path">The normalized, relative path to serialize the data to.</param>
        internal static void Save(object data, string path)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (FileStream fileStream = new FileStream(path, FileMode.Create)) 
            using (StreamWriter streamWriter = new StreamWriter(fileStream)) { streamWriter.Write(jsonString); }
        }

        /// <summary>
        /// Reads a Json string from a file and converts it to a .NET type.
        /// </summary>
        internal static T Load<T>(string path)
        {
            string jsonString;
            T data;

            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                jsonString = streamReader.ReadToEnd();
                data = JsonConvert.DeserializeObject<T>(jsonString);
            }

            return data;
        }
    }
}
