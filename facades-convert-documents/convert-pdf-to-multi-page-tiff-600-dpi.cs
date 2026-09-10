using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // needed for Resolution

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

        try
        {
            // PdfConverter implements IDisposable, so wrap it in a using block
            using (PdfConverter converter = new PdfConverter())
            {
                // Set the desired resolution (600 DPI) for detailed graphics extraction.
                // PdfConverter.Resolution expects an Aspose.Pdf.Devices.Resolution object.
                converter.Resolution = new Resolution(600);

                // Bind the source PDF file to the converter
                converter.BindPdf(inputPath);

                // Initialize conversion process
                converter.DoConvert();

                // Convert all pages to a single multi‑page TIFF file
                converter.SaveAsTIFF(outputPath);
            }

            Console.WriteLine($"TIFF image saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
