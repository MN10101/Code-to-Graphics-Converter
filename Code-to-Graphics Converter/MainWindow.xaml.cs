using System.Threading.Tasks;
using System.Windows;

namespace CodeToGraphicsConverter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BubbleSortButton_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteAlgorithmOnBackgroundThread(async () =>
            {
                // Safely access SpeedSlider.Value on the UI thread
                int speed = 0;
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    speed = (int)SpeedSlider.Value; 
                });

                // Call the algorithm with the value from the slider
                await AlgorithmVisualizer.VisualizeBubbleSort(RenderCanvas, speed);
            });
        }


        private async void QuickSortButton_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteAlgorithmOnBackgroundThread(async () =>
            {
                // Safely access SpeedSlider.Value on the UI thread
                int speed = 0;
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    speed = (int)SpeedSlider.Value; 
                });

                // Call the algorithm with the value from the slider
                await AlgorithmVisualizer.VisualizeQuickSort(RenderCanvas, speed);
            });
        }


        private async void MergeSortButton_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteAlgorithmOnBackgroundThread(async () =>
            {
                // Safely access SpeedSlider.Value on the UI thread
                int speed = 0;
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    speed = (int)SpeedSlider.Value;
                });

                // Call the algorithm with the value from the slider
                await AlgorithmVisualizer.VisualizeMergeSort(RenderCanvas, speed);
            });
        }


        private async void HeapSortButton_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteAlgorithmOnBackgroundThread(async () =>
            {
                // Safely access SpeedSlider.Value on the UI thread
                int speed = 0;
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    speed = (int)SpeedSlider.Value; 
                });

                // Call the algorithm with the value from the slider
                await AlgorithmVisualizer.VisualizeHeapSort(RenderCanvas, speed);
            });
        }


        private void FractalButton_Click(object sender, RoutedEventArgs e)
        {
            // Run fractal generation directly on UI thread (as it seems to be a non-blocking task)
            FractalGenerator.DrawMandelbrotSet(RenderCanvas);
        }

        private void JuliaSetButton_Click(object sender, RoutedEventArgs e)
        {
            // Run fractal generation directly on UI thread
            FractalGenerator.DrawJuliaSet(RenderCanvas);
        }

        private async Task ExecuteAlgorithmOnBackgroundThread(Func<Task> algorithm)
        {
            // Running the algorithm on a background thread to keep UI responsive
            await Task.Run(async () =>
            {
                // Safely interact with the UI using Dispatcher for UI updates
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Disable buttons or perform other UI updates before the algorithm starts
                    BubbleSortButton.IsEnabled = false;
                    QuickSortButton.IsEnabled = false;
                    MergeSortButton.IsEnabled = false;
                    HeapSortButton.IsEnabled = false;
                });

                // Run the algorithm (still in the background thread)
                await algorithm();

                // After the algorithm is done, enable the buttons again and perform UI updates
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Re-enable buttons after the algorithm is complete
                    BubbleSortButton.IsEnabled = true;
                    QuickSortButton.IsEnabled = true;
                    MergeSortButton.IsEnabled = true;
                    HeapSortButton.IsEnabled = true;
                });
            });
        }
    }
}
