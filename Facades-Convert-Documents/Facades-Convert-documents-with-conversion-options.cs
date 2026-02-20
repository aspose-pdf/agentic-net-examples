using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output TIFF file path
        const string outputTiffPath = "output.tiff";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Create a PdfConverter instance
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdfPath);

                // Set conversion options
                converter.StartPage = 1;               // first page to convert
                converter.EndPage = converter.PageCount; // last page (all pages)
                converter.Resolution = new Resolution(150); // DPI; higher = better quality, slower

                // Prepare the converter (required before saving)
                converter.DoConvert();

                // Convert pages to a single multi‑page TIFF file
                converter.SaveAsTIFF(outputTiffPath);
            }

            Console.WriteLine($"Conversion completed successfully. TIFF saved to '{outputTiffPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}