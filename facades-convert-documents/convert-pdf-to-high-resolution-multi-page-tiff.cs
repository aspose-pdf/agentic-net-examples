using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Create a PdfConverter instance and set the desired resolution (400 DPI)
            using (PdfConverter converter = new PdfConverter())
            {
                converter.Resolution = new Resolution(400);   // 400 DPI for archival quality
                converter.BindPdf(inputPdf);                  // Bind the source PDF
                converter.DoConvert();                        // Prepare conversion

                // Save all pages as a single multi‑page TIFF file
                converter.SaveAsTIFF(outputTiff);
                converter.Close(); // Release internal resources
            }

            Console.WriteLine($"PDF successfully converted to TIFF: '{outputTiff}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}