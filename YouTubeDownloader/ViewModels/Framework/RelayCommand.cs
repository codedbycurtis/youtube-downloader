using System;
using System.Windows.Input;

namespace YouTubeDownloader.ViewModels.Framework
{
    /// <summary>
    /// A simple command that performs an action.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Members

        private Action _action;

        #endregion

        #region Properties

        /// <summary>
        /// Invoked when the <see cref="CanExecute(object)"/> property changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Can the command execute?
        /// </summary>
        /// <returns>True -- a <see cref="RelayCommand"/> can always execute.</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the <see cref="_action"/>.
        /// </summary>
        public void Execute(object parameter) => _action();

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand"/> with the specified <paramref name="action"/>.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public RelayCommand(Action action) => _action = action;

        #endregion
    }

    /// <summary>
    /// A simple command that performs an action with a specified parameter.
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        #region Members

        private Action<T> _action;

        #endregion

        #region Properties

        /// <summary>
        /// Invoked when the <see cref="CanExecute(object)"/> property changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Can the command execute?
        /// </summary>
        /// <returns>True -- a <see cref="RelayCommand{T}"/> can always execute.</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the <see cref="_action"/> with the specified <paramref name="parameter"/>.
        /// </summary>
        public void Execute(object parameter) => _action((T)parameter);

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand{T}"/> with the specified <paramref name="action"/>.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public RelayCommand(Action<T> action) => _action = action;

        #endregion
    }
}