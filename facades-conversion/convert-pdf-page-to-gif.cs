using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string gifPath = "page2.gif";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfConverter handles PDF to image conversion in a cross‑platform way.
        var converter = new PdfConverter();
        try
        {
            // Bind the source PDF.
            converter.BindPdf(pdfPath);

            // Restrict conversion to page 2 only.
            converter.StartPage = 2;
            converter.EndPage   = 2;

            // Optional: set resolution for better quality.
            converter.Resolution = new Aspose.Pdf.Devices.Resolution(150);

            // Prepare the conversion.
            converter.DoConvert();

            // Convert the selected page to a GIF image.
            // GetNextImage writes the image to the supplied path using the specified format.
            converter.GetNextImage(gifPath, ImageFormat.Gif);

            Console.WriteLine($"Page 2 saved as GIF to '{gifPath}'.");
        }
        finally
        {
            // Release any resources held by the converter.
            converter.Close();
        }
    }
}
