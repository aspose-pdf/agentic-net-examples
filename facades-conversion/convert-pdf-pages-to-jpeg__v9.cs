using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Pages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – ensure deterministic disposal
        using (var converter = new PdfConverter())
        {
            // Load and bind the PDF document
            converter.BindPdf(inputPdf);

            // Define the page range to convert (pages 1 through 12)
            converter.StartPage = 1;
            converter.EndPage   = 12;

            // Optional: set resolution (default is 96 DPI). Adjust as required.
            converter.Resolution = new Resolution(150);

            // Prepare the conversion – no arguments are required.
            converter.DoConvert();

            // Convert each page in the range to a JPEG image using the HasNextImage/GetNextImage pattern.
            int page = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Page_{page}.jpg");
                converter.GetNextImage(outPath, ImageFormat.Jpeg);
                Console.WriteLine($"Saved page {page} as JPEG to '{outPath}'.");
                page++;
            }
        }
    }
}
