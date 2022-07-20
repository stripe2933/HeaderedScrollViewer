using ReactiveUI;

namespace HeaderedScrollViewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string? _currentHeaderText;

        public string? CurrentHeaderText
        {
            get => _currentHeaderText;
            set => this.RaiseAndSetIfChanged(ref _currentHeaderText, value);
        }
    }
}