using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CodeToGraphicsConverter
{
    public static class FractalGenerator
    {
        public static void DrawMandelbrotSet(Canvas canvas)
        {
            canvas.Children.Clear();
            int width = (int)canvas.ActualWidth;
            int height = (int)canvas.ActualHeight;

            WriteableBitmap bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr32, null);
            int stride = bitmap.PixelWidth * (bitmap.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[bitmap.PixelHeight * stride];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double a = (x - width / 2.0) * 4.0 / width;
                    double b = (y - height / 2.0) * 4.0 / height;

                    double ca = a, cb = b;
                    int n = 0;

                    while (n < 255 && (a * a + b * b) <= 4.0)
                    {
                        double tempA = a * a - b * b + ca;
                        b = 2 * a * b + cb;
                        a = tempA;
                        n++;
                    }

                    int color = n == 255 ? 0 : n * 255 / 50;
                    int index = y * stride + x * 4;
                    // Blue
                    pixels[index + 0] = (byte)(color);
                    // Green
                    pixels[index + 1] = (byte)(color);
                    // Red
                    pixels[index + 2] = (byte)(color);
                    // Alpha
                    pixels[index + 3] = 255;           
                }
            }

            bitmap.WritePixels(new System.Windows.Int32Rect(0, 0, width, height), pixels, stride, 0);
            Image image = new Image { Source = bitmap };
            canvas.Children.Add(image);
        }

        public static void DrawJuliaSet(Canvas canvas)
        {
            canvas.Children.Clear();
            int width = (int)canvas.ActualWidth;
            int height = (int)canvas.ActualHeight;

            WriteableBitmap bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr32, null);
            int stride = bitmap.PixelWidth * (bitmap.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[bitmap.PixelHeight * stride];

            double ca = -0.7, cb = 0.27015;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double a = (x - width / 2.0) * 4.0 / width;
                    double b = (y - height / 2.0) * 4.0 / height;
                    int n = 0;

                    while (n < 255 && (a * a + b * b) <= 4.0)
                    {
                        double tempA = a * a - b * b + ca;
                        b = 2 * a * b + cb;
                        a = tempA;
                        n++;
                    }

                    int color = n == 255 ? 0 : n * 255 / 50;
                    int index = y * stride + x * 4;
                    // Blue
                    pixels[index + 0] = (byte)(color);
                    // Green
                    pixels[index + 1] = (byte)(color);
                    // Red
                    pixels[index + 2] = (byte)(color);
                    // Alpha
                    pixels[index + 3] = 255;           
                }
            }

            bitmap.WritePixels(new System.Windows.Int32Rect(0, 0, width, height), pixels, stride, 0);
            Image image = new Image { Source = bitmap };
            canvas.Children.Add(image);
        }
    }
}
