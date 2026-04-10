using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Convert pages 2‑6 of the PDF to BMP images using PdfConverter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPath);

            // Set the desired page range (1‑based indexing)
            converter.StartPage = 2;
            converter.EndPage   = 6;

            // Initialize conversion
            converter.DoConvert();

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                // Save the current page as BMP
                converter.GetNextImage(outPath, System.Drawing.Imaging.ImageFormat.Bmp);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}