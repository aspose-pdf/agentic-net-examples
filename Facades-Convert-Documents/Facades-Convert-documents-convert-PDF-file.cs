using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output image path (TIFF)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Program <input.pdf> <output.tiff>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (creation rule)
            Document pdfDocument = new Document(inputPath);

            // Determine the number of pages to convert
            int pageCount = pdfDocument.Pages.Count;

            // Use the Facades PdfConverter to convert the PDF pages to an image format
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document to the converter
                converter.BindPdf(pdfDocument);

                // Set conversion range (all pages) and resolution
                converter.StartPage = 1;
                converter.EndPage = pageCount;
                converter.Resolution = new Resolution(300); // 300 DPI

                // Perform the conversion
                converter.DoConvert();

                // Save the result as a multi‑page TIFF image (save rule)
                converter.SaveAsTIFF(outputPath);
            }

            // Optional: confirm success
            Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}