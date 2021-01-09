using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YouTubeDownloader
{
    /// <summary>
    /// A base ViewModel that fires <see cref="PropertyChanged"/> events on demand.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when a property's value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires a <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property whose value has changed.</param>
        public void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Sets a property's value.
        /// </summary>
        /// <typeparam name="T">The property's <see cref="System.Type"/>.</typeparam>
        /// <param name="property">The referenced property to update.</param>
        /// <param name="updatedValue">The updated value of the property.</param>
        /// <param name="propertyName">The name of the property to update.</param>
        public void SetProperty<T>(ref T property, T updatedValue, [CallerMemberName]string propertyName = "")
        {
            if (!Equals(property, updatedValue))
            {
                property = updatedValue;
                NotifyPropertyChanged(propertyName);
            }
        }
    }
}
