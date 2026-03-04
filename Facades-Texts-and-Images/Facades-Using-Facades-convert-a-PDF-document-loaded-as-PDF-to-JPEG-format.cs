using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPdf = "input.pdf";

        // Directory where JPEG images will be saved
        const string outputDir = "OutputImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter facade to convert PDF pages to images
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document
            converter.BindPdf(inputPdf);

            // Perform any required initialization before extracting images
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate over all pages and save each as a JPEG file
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.jpg");
                // Save the current page as JPEG (default quality)
                converter.GetNextImage(outputPath, ImageFormat.Jpeg);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}