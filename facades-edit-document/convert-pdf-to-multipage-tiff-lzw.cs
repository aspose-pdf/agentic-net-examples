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
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (disposed automatically)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create and configure the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF document to the converter
                converter.BindPdf(pdfDoc);
                // Perform any necessary initialization
                converter.DoConvert();
                // Save all pages as a multi‑page TIFF using LZW compression
                converter.SaveAsTIFF(outputPath, CompressionType.LZW);
            }
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputPath}");
    }
}