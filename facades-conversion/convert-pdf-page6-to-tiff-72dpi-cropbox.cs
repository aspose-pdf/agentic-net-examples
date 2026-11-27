using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page6.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize the PdfConverter facade
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(pdfDoc);

            // Set conversion resolution to 72 DPI
            converter.Resolution = new Resolution(72);

            // Use the CropBox as the coordinate reference (PageCoordinateType enum)
            converter.CoordinateType = PageCoordinateType.CropBox;

            // Convert only page 6
            converter.StartPage = 6;
            converter.EndPage   = 6;

            // Perform conversion and save the result as a TIFF image
            converter.DoConvert();               // optional, ensures conversion is performed
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"Page 6 saved as TIFF to '{outputPath}'.");
    }
}
