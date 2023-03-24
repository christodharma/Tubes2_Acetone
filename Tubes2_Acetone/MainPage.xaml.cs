using System.ComponentModel;

namespace Tubes2_Acetone;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new MainViewModel();
	}
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged(nameof(FilePath));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [Obsolete]
    private async void OnChooseFileClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.plain-text" } },
            { DevicePlatform.Android, new[] { "text/plain" } },
            { DevicePlatform.UWP, new[] { ".txt" } },
        })
        });

        if (result != null)
        {
            string FilePath = result.FullPath;
            // Do something with the selected file
            ((MainViewModel)BindingContext).FilePath = result.FullPath;
        }
    }
}

