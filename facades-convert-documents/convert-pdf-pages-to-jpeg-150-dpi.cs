using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades; // includes PdfConverter, etc.
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        // Create and configure the PdfConverter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the loaded document to the converter
            converter.BindPdf(pdfDoc);

            // Set the page range (1‑based indexing)
            converter.StartPage = 1;
            converter.EndPage   = 10;

            // NOTE: CoordinateType enum is not available in the current Aspose.Pdf version.
            // The converter uses CropBox coordinates by default, so we omit the assignment.

            // Set resolution to 150 DPI (default is 150, set explicitly for clarity)
            converter.Resolution = new Resolution(150);

            // Prepare the converter for conversion
            converter.DoConvert();

            int pageIndex = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.jpg");
                // Save the current page as JPEG (using System.Drawing.Imaging.ImageFormat)
                converter.GetNextImage(outputPath, ImageFormat.Jpeg);
                pageIndex++;
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
