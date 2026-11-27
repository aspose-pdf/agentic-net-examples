using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

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

        // Create and configure the PdfConverter
        var converter = new PdfConverter();
        converter.BindPdf(inputPath);

        // Convert all pages (1‑based indexing)
        converter.StartPage = 1;
        converter.EndPage = converter.PageCount;

        // Set DPI resolution – use a Resolution object, not an int property
        converter.Resolution = new Resolution(300);

        // Prepare the conversion (no arguments)
        converter.DoConvert();

        // Write each page as a BMP image using the HasNextImage/GetNextImage loop
        int pageNumber = converter.StartPage;
        while (converter.HasNextImage())
        {
            string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
            converter.GetNextImage(outputPath, ImageFormat.Bmp);
            pageNumber++;
        }

        // Release resources
        converter.Close();

        Console.WriteLine("Conversion completed: BMP images saved to " + outputDir);
    }
}
