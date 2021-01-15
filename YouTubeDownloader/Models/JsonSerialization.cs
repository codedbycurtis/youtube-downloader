using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace YouTubeDownloader
{
    /// <summary>
    /// Provides methods for serializing and deserializing data in the Json format.
    /// </summary>
    internal static class JsonSerialization
    {
        /// <summary>
        /// Converts a .NET object to a Json string and writes it to a file.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <param name="path">The normalized, relative path to serialize the data to.</param>
        internal static void Serialize(object data, string path)
        {
            string jsonString = JsonConvert.SerializeObject(data);

            using (FileStream fileStream = new FileStream(path, FileMode.Create)) 
            using (StreamWriter streamWriter = new StreamWriter(fileStream)) 
            { streamWriter.Write(jsonString); }
        }

        /// <summary>
        /// Reads a Json string from a file and converts it to a .NET object.
        /// </summary>
        internal static object Deserialize(string path)
        {
            string jsonString;
            object data;

            if (string.IsNullOrEmpty(path) || !File.Exists(path)) 
                throw new FileNotFoundException($"Read error: The file '{path}' could not be found.", path);

            using (FileStream fileStream = new FileStream(path, FileMode.Open)) 
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                jsonString = streamReader.ReadToEnd();
                data = JsonConvert.DeserializeObject(jsonString);
            }

            return data;
        }
    }
}
