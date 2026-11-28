using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bmpImage  = "image.bmp";
        const int    pageNumber = 16;
        const double x = 50;
        const double y = 50;
        // Optional: set the desired image size (in points). Adjust as needed.
        const float  imgWidth  = 200f;
        const float  imgHeight = 200f;

        // Verify required files exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bmpImage))
        {
            Console.Error.WriteLine($"BMP image not found: {bmpImage}");
            return;
        }

        // PdfFileMend does not implement IDisposable, so no using block is needed.
        PdfFileMend mend = new PdfFileMend();

        // Bind the source PDF document.
        mend.BindPdf(inputPdf);

        // Add the BMP image to the specified page.
        // Use the overload that accepts an int[] of page numbers and also requires width/height.
        int[] pages = new[] { pageNumber };
        mend.AddImage(bmpImage, pages, (float)x, (float)y, imgWidth, imgHeight);

        // Save the modified PDF.
        mend.Save(outputPdf);

        Console.WriteLine($"Image added and saved to '{outputPdf}'.");
    }
}
