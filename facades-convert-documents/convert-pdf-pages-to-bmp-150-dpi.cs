using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

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

        // Use PdfConverter (Facade) to convert pages to BMP images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Set the desired resolution (150 DPI)
            converter.Resolution = new Resolution(150);

            // Define the page range (pages 1 through 20)
            converter.StartPage = 1;
            converter.EndPage   = 20;

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageNumber = converter.StartPage;

            // Extract each page as a BMP image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}_out.bmp");
                // Use System.Drawing.Imaging.ImageFormat for BMP
                converter.GetNextImage(outputPath, ImageFormat.Bmp);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF pages 1‑20 have been converted to BMP images.");
    }
}
