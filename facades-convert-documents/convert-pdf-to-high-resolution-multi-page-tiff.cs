using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists (prevents runtime errors)
        string outputDir = Path.GetDirectoryName(outputTiff);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create and configure the PdfConverter
        using (PdfConverter converter = new PdfConverter())
        {
            // Set high‑resolution (400 DPI) for archival quality
            converter.Resolution = new Resolution(400);

            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Prepare the converter (required before saving)
            converter.DoConvert();

            // Convert all pages to a single multi‑page TIFF file
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"Conversion completed: {outputTiff}");
    }
}
