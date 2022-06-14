using PdfLibCore;
using PdfLibCore.Enums;
using SixLabors.ImageSharp;

var dpiX = 200D;
var dpiY = 200D;
var filename = "sample";


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



    // resize the image and save it to the output stream
    using (var outputStream = new FileStream($"{filename}-{pageNum}.jpg", FileMode.CreateNew))
    using (var image = Image.Load(stream))
    {
        image.SaveAsJpeg(outputStream);
    }
}