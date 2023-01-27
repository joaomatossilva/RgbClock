using RGBLedMatrix;

var matrix = new RGBLedMatrix.RGBLedMatrix(new RGBLedMatrixOptions { ChainLength = 1, Rows = 64, Cols = 64, GpioSlowdown = 4, Brightness = 50 });
var canvas = matrix.CreateOffscreenCanvas();
var font = new RGBLedFont("./font.bdf");

var date = DateTime.Now;

var day = date.ToString("dd-MM");
canvas.DrawText(font, 1, (10 * 1/*index*/) + 1, new Color(0, 255, 0), day);

var hour = date.ToString("HH-mm");
canvas.DrawText(font, 1 + 25 + 3, (10 * 1/*index*/) + 1, new Color(0, 255, 0), hour);

var dayOfWeek = date.ToString("ddd");
canvas.DrawText(font, 1 + 25 + 3 + 25 + 3, (10 * 1/*index*/) + 1, new Color(0, 255, 0), dayOfWeek);
                
matrix.SwapOnVsync(canvas);

while (!Console.KeyAvailable)
{
    Thread.Sleep(250);
}