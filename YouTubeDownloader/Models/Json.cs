using System.IO;
using Newtonsoft.Json;

namespace YouTubeDownloader
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
            string jsonString = JsonConvert.SerializeObject(data);

            using (FileStream fileStream = new FileStream(path, FileMode.Create)) 
            using (StreamWriter streamWriter = new StreamWriter(fileStream)) 
            { streamWriter.Write(jsonString); }
        }

        /// <summary>
        /// Reads a Json string from a file and converts it to a .NET object.
        /// </summary>
        internal static object Load(string path)
        {
            string jsonString;
            object data;

            if (string.IsNullOrEmpty(path) || !File.Exists(path)) 
                throw new FileNotFoundException();

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
