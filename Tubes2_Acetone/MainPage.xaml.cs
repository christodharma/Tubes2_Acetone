using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
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
            { DevicePlatform.macOS, new[] { "txt" } },
            { DevicePlatform.iOS, new[] { "public.plain-text" } },
            { DevicePlatform.Android, new[] { "text/plain" } },
            { DevicePlatform.UWP, new[] { ".txt" } },
        })
        });

        if (result != null)
        {
            string FilePath = result.FullPath;
            ((MainViewModel)BindingContext).FilePath = result.FullPath;
            // Do something with the selected file
            // Read the text file contents
            string fileContents = File.ReadAllText(FilePath);

            // Split the contents into lines
            string[] lines = fileContents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Iterate over the lines and characters to add Image controls to the Grid
            for (int row = 0; row < lines.Length; row++)
            {
                for (int col = 0; col < lines[row].Length; col++)
                {
                    // Create an Image control for the current character
                    Image image = new Image();
                    if (lines[row][col] == 'X')
                    {
                        // Set the image source for walls
                        image.Source = "wall";
                    }
                    else if (lines[row][col] == 'T')
                    {
                        image.Source = "treasure";
                        image.Scale = 0.1;
                    } else if (lines[row][col] == 'R')
                    {
                        image.Source = "visited";
                    }
                    /*
                    else
                    {
                        // Set the image source for other objects
                        image.Source = "object";
                    }
                    */
                    // Add the Image control to the Grid
                    Grid.SetRow(image, row);
                    Grid.SetColumn(image, col);
                    MapGrid.Children.Add(image);
                }
            }
        }
    }
}