using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YouTubeDownloader.ViewModels.Framework
{
    /// <summary>
    /// Fires <see cref="PropertyChanged"/> events on demand.
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
        /// Updates a property's value and fires the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T">The property's type.</typeparam>
        /// <param name="backingStore">The property's current value.</param>
        /// <param name="value">The new value to set.</param>
        /// <param name="onPropertyChanged">Custom action that is executed once the property's value changes.</param>
        /// <param name="propertyName">The name of the property that called this method.</param>
        public void SetProperty<T>(ref T backingStore, T value, Action onPropertyChanged = null, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(backingStore, value))
            {
                backingStore = value;
                onPropertyChanged?.Invoke();
                NotifyPropertyChanged(propertyName);
            }
        }
    }
}