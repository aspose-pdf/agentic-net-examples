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

        PdfConverter converter = null;
        try
        {
            // Initialize the PdfConverter facade
            converter = new PdfConverter();

            // Set the desired resolution (400 DPI) using a Resolution object
            converter.Resolution = new Resolution(400);

            // Bind the source PDF file
            converter.BindPdf(inputPath);

            // Prepare the converter (required before saving)
            converter.DoConvert();

            // Convert all pages to a single multi‑page TIFF file
            converter.SaveAsTIFF(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
            return;
        }
        finally
        {
            // Release resources held by the converter
            converter?.Close();
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputPath}");
    }
}
