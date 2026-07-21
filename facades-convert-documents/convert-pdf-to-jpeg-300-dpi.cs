using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Convert PDF pages to JPEG images using PdfConverter (Facades API)
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Set resolution to 300 DPI
            converter.Resolution = new Resolution(300);

            // NOTE: In recent Aspose.Pdf versions the CropBox is used by default.
            // The CoordinateType property has been removed, so we simply omit it.

            // Initialize conversion process
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages and save each as a JPEG image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                // GetNextImage saves the current page as JPEG by default
                converter.GetNextImage(outputPath);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
