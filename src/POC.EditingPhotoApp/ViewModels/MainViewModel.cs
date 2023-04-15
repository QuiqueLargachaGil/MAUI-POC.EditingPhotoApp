using POC.EditingPhotoApp.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace POC.EditingPhotoApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private FileResult _result;
        public MainViewModel()
        {
            TakePhotoCommand = new Command(async () => await TakePhoto());
            OpenEditorCommand = new Command(async () => await OpenEditor());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand TakePhotoCommand { get; set; }
        public ICommand OpenEditorCommand { get; set; }

        private ImageSource _photo;
        public ImageSource Photo
        {
            get => _photo;
            set
            {
                _photo = value;
                OnPropertyChanged();
            }
        }

        private async Task TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                _result = null;
                _result = await MediaPicker.Default.CapturePhotoAsync();
                var stream = await _result.OpenReadAsync();
                    
                Photo = ImageSource.FromStream(() => stream);
            }
        }

        private async Task OpenEditor()
        {
            var parameters = new Dictionary<string, object>()
            {
                { "photo", _result }
            };

            await Shell.Current.GoToAsync(nameof(EditingView), parameters);
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
