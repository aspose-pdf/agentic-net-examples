using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter is a Facade that implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file to the converter
            converter.BindPdf(inputPdf);

            // Limit conversion to the first 10 pages (pages are 1‑based)
            converter.StartPage = 1;
            converter.EndPage = 10;

            // Perform any necessary initialization before extracting images
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through the generated images
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                // Save the current page as a BMP image
                converter.GetNextImage(outputPath, ImageFormat.Bmp);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}