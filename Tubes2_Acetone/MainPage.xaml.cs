using System.ComponentModel;
using Microsoft.Maui.Graphics;
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
        private string _stepSolution;
        public string StepSolution
        {
            get { return _stepSolution; }
            set
            {
                if (_stepSolution != value)
                {
                    _stepSolution = value;
                    OnPropertyChanged(nameof(StepSolution));
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
            FilePathEntry.Text = result.FullPath;
        }
        ((MainViewModel)BindingContext).StepSolution = "0";
    }
    private void OnLoadFileClicked(object sender, EventArgs e)
    {
        string filePath = FilePathEntry.Text;
        ((MainViewModel)BindingContext).FilePath = FilePathEntry.Text;

        if (!string.IsNullOrEmpty(filePath))
        {
            try
            {
                // Read the text file contents
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    MapGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                }
                int numColumns = lines[0].Split(' ').Length;
                for (int i = 0; i < numColumns; i++)
                {
                    MapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                }

                for (int row = 0; row < lines.Length; row++)
                {
                    string[] words = lines[row].Split(' ');
                    for (int col = 0; col < words.Length; col++)
                    {

                        /*var label = new Label
                        {
                            Text = words[col],
                            TextColor = Colors.Black,
                            BackgroundColor = words[col] == "X" ? Colors.Gray : Colors.White
                        };
                        MapGrid.Children.Add(label);*/


                        // Create an image for each word and add it to the grid
                        var image = new Image();
                        if (words[col] == "X")
                        {
                            image.Source = "wall.png";
                            image.BackgroundColor = Colors.Gray;
                        }
                        else if (words[col] == "T")
                        {
                            image.Source = "treasure.png";
                            image.BackgroundColor = Colors.Yellow;
                        }
                        else if (words[col] == "R")
                        {
                            image.Source = "visited.png";
                            image.BackgroundColor = Colors.Black;
                        }
                        else if (words[col] == "K")
                        {
                            image.Source = "treasure.png";
                            image.BackgroundColor = Colors.White;
                        }

                        // Add the image to the grid
                        Grid.SetRow(image, row);
                        Grid.SetColumn(image, col);
                        MapGrid.Children.Add(image);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
    }
}