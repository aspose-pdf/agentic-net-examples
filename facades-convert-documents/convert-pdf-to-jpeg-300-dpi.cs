using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade API for conversion
using Aspose.Pdf.Devices;          // Resolution enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";               // Source PDF file
        const string outputDir = "output_images";           // Folder for JPEG files

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – use a using block for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Set conversion resolution to 300 DPI
            converter.Resolution = new Resolution(300);

            // No need to set CoordinateType – CropBox is the default in recent Aspose.Pdf versions
            // converter.CoordinateType = CoordinateType.CropBox; // removed

            // Prepare the converter (required before retrieving images)
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate over all pages; each call to GetNextImage writes a JPEG file
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.jpeg");
                converter.GetNextImage(outputPath);   // Default format is JPEG
                pageIndex++;
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
