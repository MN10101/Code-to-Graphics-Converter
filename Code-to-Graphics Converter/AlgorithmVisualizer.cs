using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

public static class AlgorithmVisualizer
{
    private static async Task VisualizeWithDelay(Canvas canvas, int delay)
    {
        await Task.Delay(1000 / delay);
    }

    public static async Task VisualizeBubbleSort(Canvas canvas, int speed)
    {
        canvas.Dispatcher.Invoke(() => canvas.Children.Clear());
        int[] array = { 50, 30, 70, 10, 90, 40, 60, 20, 80 };
        VisualizeArray(canvas, array, Colors.Red);

        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    VisualizeArray(canvas, array, Colors.Red);
                    await VisualizeWithDelay(canvas, speed);
                }
            }
        }
    }

    public static async Task VisualizeQuickSort(Canvas canvas, int speed)
    {
        canvas.Dispatcher.Invoke(() => canvas.Children.Clear());
        int[] array = { 50, 30, 70, 10, 90, 40, 60, 20, 80 };
        VisualizeArray(canvas, array, Colors.Blue);
        await QuickSort(canvas, array, 0, array.Length - 1, speed);
    }

    private static async Task QuickSort(Canvas canvas, int[] array, int low, int high, int speed)
    {
        if (low < high)
        {
            int pi = Partition(canvas, array, low, high, speed);
            await QuickSort(canvas, array, low, pi - 1, speed);
            await QuickSort(canvas, array, pi + 1, high, speed);
        }
    }

    private static int Partition(Canvas canvas, int[] array, int low, int high, int speed)
    {
        int pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
                VisualizeArray(canvas, array, Colors.Blue);
                VisualizeWithDelay(canvas, speed).Wait(); 
            }
        }

        (array[i + 1], array[high]) = (array[high], array[i + 1]);
        VisualizeArray(canvas, array, Colors.Blue);
        VisualizeWithDelay(canvas, speed).Wait();
        return i + 1;
    }

    public static async Task VisualizeMergeSort(Canvas canvas, int speed)
    {
        canvas.Dispatcher.Invoke(() => canvas.Children.Clear());
        int[] array = { 50, 30, 70, 10, 90, 40, 60, 20, 80 };
        VisualizeArray(canvas, array, Colors.Green);
        await MergeSort(canvas, array, 0, array.Length - 1, speed);
    }

    private static async Task MergeSort(Canvas canvas, int[] array, int left, int right, int speed)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;
            await MergeSort(canvas, array, left, mid, speed);
            await MergeSort(canvas, array, mid + 1, right, speed);
            await Merge(canvas, array, left, mid, right, speed);
        }
    }

    private static async Task Merge(Canvas canvas, int[] array, int left, int mid, int right, int speed)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;
        int[] L = new int[n1];
        int[] R = new int[n2];

        for (int i = 0; i < n1; i++) L[i] = array[left + i];
        for (int j = 0; j < n2; j++) R[j] = array[mid + 1 + j];

        int i1 = 0, j1 = 0, k = left;
        while (i1 < n1 && j1 < n2)
        {
            array[k++] = (L[i1] <= R[j1]) ? L[i1++] : R[j1++];
            VisualizeArray(canvas, array, Colors.Green);
            await VisualizeWithDelay(canvas, speed); 
        }

        while (i1 < n1) array[k++] = L[i1++];
        while (j1 < n2) array[k++] = R[j1++];
    }

    public static async Task VisualizeHeapSort(Canvas canvas, int speed)
    {
        canvas.Dispatcher.Invoke(() => canvas.Children.Clear());
        int[] array = { 50, 30, 70, 10, 90, 40, 60, 20, 80 };
        VisualizeArray(canvas, array, Colors.Purple);
        await HeapSort(canvas, array, speed);
    }

    private static async Task HeapSort(Canvas canvas, int[] array, int speed)
    {
        int n = array.Length;

        for (int i = n / 2 - 1; i >= 0; i--)
        {
            await Heapify(canvas, array, n, i, speed);
        }

        for (int i = n - 1; i > 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            VisualizeArray(canvas, array, Colors.Purple);
            await VisualizeWithDelay(canvas, speed); 
            await Heapify(canvas, array, i, 0, speed);
        }
    }

    private static async Task Heapify(Canvas canvas, int[] array, int n, int i, int speed)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && array[left] > array[largest]) largest = left;
        if (right < n && array[right] > array[largest]) largest = right;

        if (largest != i)
        {
            (array[i], array[largest]) = (array[largest], array[i]);
            VisualizeArray(canvas, array, Colors.Purple);
            await VisualizeWithDelay(canvas, speed); 
            await Heapify(canvas, array, n, largest, speed);
        }
    }

    private static void VisualizeArray(Canvas canvas, int[] array, Color color)
    {
        // Ensure updates happen on the UI thread
        canvas.Dispatcher.Invoke(() =>
        {
            canvas.Children.Clear();
            double barWidth = canvas.ActualWidth / array.Length;

            for (int i = 0; i < array.Length; i++)
            {
                var bar = new Rectangle
                {
                    Width = barWidth - 2,
                    Height = array[i] * 2,
                    Fill = new SolidColorBrush(color)
                };
                Canvas.SetLeft(bar, i * barWidth);
                Canvas.SetBottom(bar, 0);
                canvas.Children.Add(bar);
            }
        });
    }
}
