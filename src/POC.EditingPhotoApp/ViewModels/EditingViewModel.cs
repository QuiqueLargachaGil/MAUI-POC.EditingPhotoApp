using CommunityToolkit.Maui.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace POC.EditingPhotoApp.ViewModels
{
    public class EditingViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        public EditingViewModel()
        {
            CancelCommand = new Command(async () => await Cancel());
            SaveCommand = new Command(async () => await Save());
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var result = (FileResult)query["photo"];
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                PhotoToEdit = ImageSource.FromStream(() => stream);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Func<Task<Stream>> GetEditedPhotoEvent { get; set; }

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        private ImageSource _photoToEdit;
        public ImageSource PhotoToEdit
        {
            get => _photoToEdit;
            set
            {
                _photoToEdit = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _photoToSave;
        public ImageSource PhotoToSave
        {
            get => _photoToSave;
            set
            {
                _photoToSave = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<IDrawingLine> _lines = new();
        public ObservableCollection<IDrawingLine> Lines
        {
            get => _lines;
            set
            {
                _lines = value;
                OnPropertyChanged();
            }
        }

        private async Task Cancel()
        {
            Lines.Clear();
            await Shell.Current.GoToAsync("..");
        }

        private async Task Save()
        {
            var stream = await GetEditedPhotoEvent?.Invoke();

            // TODO Capture only partial screenshot from ViewModel. Get the position from Image control with PhotoToSave binded??
            //var screenshot = await Screenshot.CaptureAsync();
            //var stream = await screenshot.OpenReadAsync();

            PhotoToSave = ImageSource.FromStream(() => stream);
            Lines.Clear();
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
