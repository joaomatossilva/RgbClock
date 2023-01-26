using RGBLedMatrix;

var matrix = new RGBLedMatrix.RGBLedMatrix(new RGBLedMatrixOptions { ChainLength = 1, Rows = 64, Cols = 64, GpioSlowdown = 4, Brightness = 50 });
var canvas = matrix.CreateOffscreenCanvas();
var font = new RGBLedFont("./font.bdf");
var text = "Hello World!";

canvas.DrawText(font, 1, (10 * 1/*index*/) + 1, new Color(0, 255, 0), text);
                
matrix.SwapOnVsync(canvas);

while (!Console.KeyAvailable)
{
    Thread.Sleep(250);
}