using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TiffPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfConverter implements IDisposable – wrap it in a using block
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdfPath);

                // Define the page range to extract (inclusive)
                converter.StartPage = 4; // first page to convert (1‑based indexing)
                converter.EndPage   = 9; // last page to convert

                // Optional: set resolution for better quality (dpi)
                converter.Resolution = new Resolution(150);

                // Perform the conversion preparation
                converter.DoConvert();

                // Save the selected pages as a (multi‑page) TIFF file
                string outputPath = Path.Combine(outputFolder, "Pages_4_to_9.tiff");
                converter.SaveAsTIFF(outputPath);
                Console.WriteLine($"Saved pages 4‑9 as TIFF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
