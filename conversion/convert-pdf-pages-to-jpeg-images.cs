using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Added for JpegDevice

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir = "OutputImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF document inside a using block for proper disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build output file name for each page
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                    // Use JpegDevice – default quality is applied when no explicit quality is set
                    var jpegDevice = new JpegDevice();

                    // Convert the current page to JPEG and save to the output path
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], outputPath);

                    Console.WriteLine($"Saved page {pageNumber} as JPEG: {outputPath}");
                }
            }

            Console.WriteLine("PDF to JPEG conversion completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
