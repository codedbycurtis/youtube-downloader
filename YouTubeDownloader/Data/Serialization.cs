using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeDownloader
{
    /// <summary>
    /// Provides methods for serializing and deserializing the <see cref="Internal.Library"/>.
    /// </summary>
    internal static class Serialization
    {
        /// <summary>
        /// Converts the <see cref="Internal.Library"/> to a Json string and writes it to a file.
        /// </summary>
        internal static void JsonSerialize()
        {
            string jsonString = JsonConvert.SerializeObject(Internal.Library);
            using (FileStream fileStream = new FileStream(Internal.LIBRARY_PATH, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
        }

        /// <summary>
        /// Reads a Json string from the <see cref="Internal.LIBRARY_PATH"/>, converts it to a <see cref="List{MediaFile}"/>,
        /// and uses it to populate the <see cref="Internal.Library"/>.
        /// </summary>
        internal static void JsonDeserialize()
        {
            using (FileStream fileStream = new FileStream(Internal.LIBRARY_PATH, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string jsonString = streamReader.ReadToEnd();
                    Internal.Library = JsonConvert.DeserializeObject<List<MediaFile>>(jsonString);
                    streamReader.Close();
                }
            }
        }
    }
}
