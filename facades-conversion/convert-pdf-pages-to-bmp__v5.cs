using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPath);

            // Define the page range to convert
            converter.StartPage = 1;
            converter.EndPage   = 3;

            // Set desired resolution (dpi)
            converter.Resolution = new Resolution(150);

            // Prepare the conversion
            converter.DoConvert();

            // Export each page as a BMP image
            int page = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Page_{page}.bmp");
                converter.GetNextImage(outPath, ImageFormat.Bmp);
                page++;
            }
        }

        Console.WriteLine("Pages 1‑3 have been converted to BMP files.");
    }
}
