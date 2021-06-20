using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace YouTubeDownloader.Models
{
    /// <summary>
    /// An <see cref="ObservableCollection{T}"/> with methods for saving and loading JSON strings.
    /// </summary>
    public partial class Library : ObservableCollection<LibraryVideo>
    {
        /// <summary>
        /// Saves the current <see cref="Library"/> to the <see cref="App.LibraryFilePath"/>.
        /// </summary>
        public void Save()
        {
            using (var fileStream = File.Create(App.LibraryFilePath))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
            }
        }
    }

    /// <inheritdoc />
    public partial class Library
    {
        /// <summary>
        /// Loads a <see cref="Library"/> from the <see cref="App.LibraryFilePath"/>.
        /// </summary>
        /// <returns>The loaded <see cref="Library"/>.</returns>
        public static Library Load()
        {
            using (var fileStream = File.OpenRead(App.LibraryFilePath))
            using (var streamReader = new StreamReader(fileStream))
            {
                var jsonString = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<Library>(jsonString);
            }
        }
    }
}
