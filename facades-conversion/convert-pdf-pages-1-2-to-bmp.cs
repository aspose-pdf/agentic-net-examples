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
        const string outputDir = "OutputImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (var converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPath);

            // Define the page range (pages 1‑2) and optional resolution
            converter.StartPage = 1;
            converter.EndPage   = 2;
            converter.Resolution = new Resolution(150); // DPI – adjust as needed

            // Prepare the conversion
            converter.DoConvert();

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
                converter.GetNextImage(outPath, ImageFormat.Bmp);
                Console.WriteLine($"Saved page {pageNumber} as BMP to '{outPath}'.");
                pageNumber++;
            }

            converter.Close();
        }
    }
}
