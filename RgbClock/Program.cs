using System.Reflection;
using RGBLedMatrix;
using SkiaSharp;

var matrix = new RGBLedMatrix.RGBLedMatrix(new RGBLedMatrixOptions { ChainLength = 1, Rows = 64, Cols = 64, GpioSlowdown = 4, Brightness = 50 });
var canvas = matrix.CreateOffscreenCanvas();
var font = new RGBLedFont("./font.bdf");

var date = DateTime.Now;

var day = date.ToString("dd-MM");
canvas.DrawText(font, 1, (5 * 1/*index*/) + 1, new Color(120, 120, 120), day);

var hour = date.ToString("HH:mm");
canvas.DrawText(font, 1 + 20 + 3, (5 * 1/*index*/) + 1, new Color(235, 235, 235), hour);

var dayOfWeek = date.ToString("ddd").ToUpper();
canvas.DrawText(font, 1 + 20 + 5 + 20 + 5, (5 * 1/*index*/) + 1, new Color(255, 120, 120), dayOfWeek);

canvas.DrawLine(1, 7, 62, 7, new Color(50, 50, 50));
                
//sun
Assembly assembly = typeof(Program).GetTypeInfo().Assembly;
SKBitmap image;
using (Stream stream = assembly.GetManifestResourceStream("sun20x20.bmp"))
{
    image = SKBitmap.FromImage(SKImage.FromEncodedData(stream));
}

var bytes = image.Pixels.Select(pixel => new[] {pixel.Red, pixel.Green, pixel.Blue})
    .SelectMany(x => x)
    .ToArray();

DrawImage(canvas, bytes, 1, 20, image.Width, image.Height);


matrix.SwapOnVsync(canvas);

while (!Console.KeyAvailable)
{
    Thread.Sleep(250);
}

void DrawImage(RGBLedCanvas canvas, byte[] bytes, int xPos, int yPos, int width, int height)
{
    for (int y = 0; y < height; y++)
    for (int x = 0; x < width; x++)
    {
        var arrayOffset = y * width * 3 + x * 3;
        var color = new Color(bytes[arrayOffset], bytes[arrayOffset +1], bytes[arrayOffset+2]);
        // if(y == 1)
        //     Console.WriteLine($"({x},{y}-{arrayOffset}) {bytes[arrayOffset]},{bytes[arrayOffset+1]},{bytes[arrayOffset+2]}");
        canvas.SetPixel(xPos + x, yPos + y, color);
    }
}