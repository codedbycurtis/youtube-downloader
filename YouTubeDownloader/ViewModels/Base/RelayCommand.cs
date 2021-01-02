using System;
using System.Windows.Input;

namespace YouTubeDownloader
{
    /// <summary>
    /// A basic command that performs an action.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Properties

        /// <summary>
        /// The action to execute.
        /// </summary>
        private Action _action;

        #endregion

        #region Public Events

        /// <summary>
        /// The event that is invoked when the <see cref="CanExecute(object)"/> property changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Public Command Methods

        /// <summary>
        /// Whether or not the command can execute.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True; a Relay Command can always execute.</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the <see cref="_action"/>
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => _action();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action) => _action = action;

        #endregion
    }
}
