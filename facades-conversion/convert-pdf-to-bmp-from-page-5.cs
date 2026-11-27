using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPdf);

            // Convert from page 5 to the last page
            converter.StartPage = 5;
            converter.EndPage   = converter.PageCount; // all remaining pages

            // Optional: set rasterization resolution
            converter.Resolution = new Resolution(150);

            // Prepare the conversion
            converter.DoConvert();

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
                converter.GetNextImage(outPath, ImageFormat.Bmp);
                Console.WriteLine($"Saved page {pageNumber} as BMP → {outPath}");
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
