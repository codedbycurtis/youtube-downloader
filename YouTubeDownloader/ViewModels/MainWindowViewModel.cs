using System.Windows.Input;
using System.Windows.Media;

namespace YouTubeDownloader
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Private Properties

        private static bool mIsSearchHighlighted;

        private static bool mIsLibraryHighlighted;

        #endregion

        #region Public Properties

        public bool IsSearchHighlighted
        {
            get => mIsSearchHighlighted;

            set
            {
                if (value == mIsSearchHighlighted) { return; }

                mIsSearchHighlighted = value;

                NotifyPropertyChanged(nameof(IsSearchHighlighted));
            }
        }

        public bool IsLibraryHighlighted
        {
            get => mIsLibraryHighlighted;

            set
            {
                if (value == mIsLibraryHighlighted) { return; }

                mIsLibraryHighlighted = value;

                NotifyPropertyChanged(nameof(IsLibraryHighlighted));
            }
        }

        public SolidColorBrush SearchButtonContentColour
        {
            get
            {
                if (IsSearchHighlighted)
                {
                    return new SolidColorBrush(Colors.White);
                }

                return new SolidColorBrush(Colors.DarkGray);
            }
        }

        public SolidColorBrush LibraryButtonContentColour
        {
            get
            {
                if (IsLibraryHighlighted)
                {
                    return new SolidColorBrush(Colors.White);
                }

                return new SolidColorBrush(Colors.DarkGray);
            }
        }

        #endregion

        #region Commands

        public ICommand SearchButton { get; set; }
        
        public ICommand LibraryButton { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowViewModel()
        {
            // Initialise commands
            SearchButton = new RelayCommand(() => 
            {
                IsSearchHighlighted = true;
                IsLibraryHighlighted = false;
                NotifyPropertyChanged(nameof(SearchButtonContentColour));
                NotifyPropertyChanged(nameof(LibraryButtonContentColour));
            });

            LibraryButton = new RelayCommand(() => 
            {
                IsSearchHighlighted = false;
                IsLibraryHighlighted = true;
                NotifyPropertyChanged(nameof(SearchButtonContentColour));
                NotifyPropertyChanged(nameof(LibraryButtonContentColour));
            });

            IsSearchHighlighted = true;
        }

        #endregion
    }
}
