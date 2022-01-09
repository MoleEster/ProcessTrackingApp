using ProcessTrackigWPF.Models;
using System.Windows;
using System.Windows.Input;

namespace ProcessTrackigWPF.ViewModels
{
    class ViewModel: ViewModelBase
    {

        #region Helpers
        protected void SwitchTab(in ViewModelBase swithTo)
        {
            CurrentView = swithTo;
        }

        #endregion

        public ViewModel()
        {
            _closeWindowButton_Clicked = new DelegateCommand(On_CloseWindowButton_Clicked);
            _maximazeWindowButton_Clicked = new DelegateCommand(On_MaximazeWindowButton_Clicked);
            _minimazeWindowButton_Clicked = new DelegateCommand(On_MinimazeWindowButton_Clicked);
            _goTo_CurrentActivityPage = new DelegateCommand(GoTo_CurrentActivityPage_Clicked);
            _goTo_ReportPage_Clicked = new DelegateCommand(GoTo_ReportPage_Clicked_Clicked);
        }

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentView
        {
            get
            {
                if (_currentViewModel != null)
                    return _currentViewModel;
                else
                    return new MainViewModel();
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private readonly DelegateCommand _minimazeWindowButton_Clicked;
        private readonly DelegateCommand _maximazeWindowButton_Clicked;
        private readonly DelegateCommand _closeWindowButton_Clicked;
        private readonly DelegateCommand _goTo_ReportPage_Clicked;
        private readonly DelegateCommand _goTo_CurrentActivityPage;
        private readonly DelegateCommand _on_WindowClosing;

        public ICommand MinimazeWindowButton_Clicked => _minimazeWindowButton_Clicked;

        public ICommand MaximazeWindowButton_Clicked => _maximazeWindowButton_Clicked;

        public ICommand CloseWindowButton_Clicked => _closeWindowButton_Clicked;

        public ICommand GoTo_ReportPage => _goTo_ReportPage_Clicked;

        public ICommand GoTo_CurrentActivityPage => _goTo_CurrentActivityPage;

        public ICommand WindowClosing => _on_WindowClosing;

        private void GoTo_ReportPage_Clicked_Clicked(object sender)
        {
            SwitchTab(new ReportPageViewModel());
        }
        private void GoTo_CurrentActivityPage_Clicked(object sender)
        {
            SwitchTab(new CurrentActivityPageViewModel());
        }

        private void On_MinimazeWindowButton_Clicked(object sender)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void On_MaximazeWindowButton_Clicked(object sender)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void On_CloseWindowButton_Clicked(object sender)
        {
            Application.Current.MainWindow.Close();
        }

        private void On_WindowClosing(object sender)
        {

        }

    }
}
