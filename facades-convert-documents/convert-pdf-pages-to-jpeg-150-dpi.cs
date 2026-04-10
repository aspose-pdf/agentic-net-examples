using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;   // for Resolution

class PdfToJpegConverter
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFolder  = "JpegPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (Facade) to convert pages to JPEG images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdfPath);

            // Set the page range (1‑based indexing)
            converter.StartPage = 1;
            converter.EndPage   = 10;

            // No need to set CoordinateType – CropBox is the default in recent Aspose.Pdf versions.

            // Set resolution to 150 DPI (default is 150, set explicitly for clarity)
            converter.Resolution = new Resolution(150);

            // Prepare the converter
            converter.DoConvert();

            int pageIndex = 1;
            while (converter.HasNextImage())
            {
                // Build output file name (e.g., page_1.jpg, page_2.jpg, ...)
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");

                // Save the current page as JPEG (default image format is JPEG)
                converter.GetNextImage(outputPath);

                pageIndex++;
            }
        }

        Console.WriteLine($"Pages 1‑10 have been converted to JPEG images in '{outputFolder}'.");
    }
}
