using Prism.Mvvm;

namespace Jaxx.Net.Launch.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Process Launcher";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
