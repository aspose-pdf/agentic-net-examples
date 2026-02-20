using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Required for Resolution

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF file path and output TIFF file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: ConvertPdf <input.pdf> <output.tiff>");
            return;
        }

        string inputPdf = args[0];
        string outputTiff = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Create the PdfConverter facade (creation step)
            using (PdfConverter converter = new PdfConverter())
            {
                // Load the PDF document into the converter (load step)
                converter.BindPdf(inputPdf);

                // Optional: set a higher resolution for better image quality
                converter.Resolution = new Resolution(150); // default is 150 DPI

                // Convert all pages to a single multi‑page TIFF file (save step)
                converter.SaveAsTIFF(outputTiff);
            }

            Console.WriteLine($"Conversion successful. TIFF saved to: {outputTiff}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during conversion
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
