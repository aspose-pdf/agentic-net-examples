using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a TiffDevice with default settings (300 DPI)
            var tiffDevice = new TiffDevice(new Resolution(300));

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // Build the output file path for the current page
                string outPath = Path.Combine(outputDir, $"Page_{i}.tiff");

                // Convert the current page to a single‑page TIFF image
                tiffDevice.Process(pdfDoc, i, i, outPath);
            }
        }

        Console.WriteLine("PDF successfully converted to TIFF images.");
    }
}
