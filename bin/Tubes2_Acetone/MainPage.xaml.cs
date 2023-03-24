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
        private string _nodeSolution;
        public string NodeSolution
        {
            get { return _nodeSolution; }
            set
            {
                if (_nodeSolution != value)
                {
                    _nodeSolution = value;
                    OnPropertyChanged(nameof(NodeSolution));
                }
            }
        }
        private string _executionTimeSolution;
        public string ExecutionTimeSolution
        {
            get { return _executionTimeSolution; }
            set
            {
                if (_executionTimeSolution != value)
                {
                    _executionTimeSolution = value;
                    OnPropertyChanged(nameof(ExecutionTimeSolution));
                }
            }
        }
        private string _routeSolution;
        public string RouteSolution
        {
            get { return _routeSolution; }
            set
            {
                if (_routeSolution != value)
                {
                    _routeSolution = value;
                    OnPropertyChanged(nameof(RouteSolution));
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
    }
    private void OnLoadFileClicked(object sender, EventArgs e)
    {
        string filePath = FilePathEntry.Text;
        ((MainViewModel)BindingContext).FilePath = FilePathEntry.Text;
        ((MainViewModel)BindingContext).StepSolution = "0";
        ((MainViewModel)BindingContext).RouteSolution = "None";
        ((MainViewModel)BindingContext).NodeSolution = "0";
        ((MainViewModel)BindingContext).ExecutionTimeSolution = "0 ms";
        if (!string.IsNullOrEmpty(filePath))
        {
            try
            {
                MapGrid.Children.Clear();
                MapGrid.RowDefinitions.Clear();
                MapGrid.ColumnDefinitions.Clear();
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

                        image.Aspect = Aspect.AspectFit;

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
    private void OnRunClicked(object sender, EventArgs e)
    {
        try 
        {
            string filePath = FilePathEntry.Text;
            Node[,] Nodes = InputMethods.InputFile(filePath);
            Node[] NodeSolution;
            string[,] NodesAsString = InputMethods.TXTtoArray(filePath);
            ((MainViewModel)BindingContext).NodeSolution = Algorithm.CountNodes(Nodes).ToString();
            if (BFSChoice.IsChecked)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                NodeSolution = Algorithm.BFSSearch(Nodes, InputMethods.CountTreasure(NodesAsString));
                string _RouteSolution = string.Join(" - ", Algorithm.GetRoute(NodeSolution));
                int _StepSolution = Algorithm.CountSteps(NodeSolution);
                ((MainViewModel)BindingContext).StepSolution = _StepSolution.ToString();
                ((MainViewModel)BindingContext).RouteSolution = _RouteSolution;
                watch.Stop();
                ((MainViewModel)BindingContext).ExecutionTimeSolution = watch.ElapsedMilliseconds.ToString();
                /*MapGrid.Children.Clear();*/
                UpdateImageGrid(MapVisualizer.StringToVisualize(Nodes, NodeSolution), MapGrid);
            }
            else if (DFSChoice.IsChecked)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                NodeSolution = Algorithm.DFSSearch(Nodes, InputMethods.CountTreasure(NodesAsString));
                string _RouteSolution = string.Join(" - ", Algorithm.GetRoute(NodeSolution));
                int _StepSolution = Algorithm.CountSteps(NodeSolution);
                ((MainViewModel)BindingContext).StepSolution = _StepSolution.ToString();
                ((MainViewModel)BindingContext).RouteSolution = _RouteSolution;
                watch.Stop();
                ((MainViewModel)BindingContext).ExecutionTimeSolution = watch.ElapsedMilliseconds.ToString();
                /*MapGrid.Children.Clear();*/
                UpdateImageGrid(MapVisualizer.StringToVisualize(Nodes, NodeSolution), MapGrid);
            }
            else
            {
                //nothing is checked, nothing happens
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Error : {ex.Message}");
        }
    }
    private static void UpdateImageGrid(string[,] map, Grid mapGrid)
    { 
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                Image image = (Image)mapGrid.Children[row * map.GetLength(1) + col];
                switch (map[row, col])
                {
                    case "X":
                        image.Source = "wall.png";
                        image.BackgroundColor = Colors.Gray;
                        break;
                    case "T":
                        image.Source = "treasure.png";
                        image.BackgroundColor = Colors.Green;
                        break;
                    case "R":
                        image.Source = null;
                        image.BackgroundColor = Colors.Green;
                        break;
                    case "K":
                        image.Source = null;
                        image.BackgroundColor = Colors.Blue;
                        break;
                    case "A":
                        image.Source = null;
                        image.BackgroundColor = Colors.Green;
                        break;
                    case "B":
                        image.Source = null;
                        image.BackgroundColor = Colors.Green;
                        break;
                    case "C":
                        image.Source = null;
                        image.BackgroundColor = Colors.Green;
                        break;

                    default:
                        image.Source = null;
                        image.BackgroundColor = Colors.Green;
                        break;
                }
            }
        }
    }

}