

using System.Drawing;
using System.Drawing.Imaging;
using PdfLibCore;
using PdfLibCore.Enums;

Console.WriteLine("Hello, World!");
var dpiX = 200D;
var dpiY = 200D;
var filename = "s1";


var pageNum = 0;
using var pdfDocument = new PdfDocument(File.Open( $"{filename}.pdf", FileMode.Open));
foreach (var page in pdfDocument.Pages)
{   pageNum ++;
    using var pdfPage = page;
    var pageWidth = (int) (dpiX * pdfPage.Size.Width / 72);
    var pageHeight = (int) (dpiY * pdfPage.Size.Height / 72);

    using var bitmap = new PdfiumBitmap(pageWidth, pageHeight, true);
    pdfPage.Render(bitmap, PageOrientations.Normal, RenderingFlags.LcdText);
    using var stream = bitmap.AsBmpStream(dpiX, dpiY);

    Image img = System.Drawing.Image.FromStream(stream);

    img.Save($"{filename}-{pageNum}.jpg", ImageFormat.Jpeg);
}