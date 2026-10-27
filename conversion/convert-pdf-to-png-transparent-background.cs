using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PagesAsPng";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate using 1‑based page indexing (Aspose.Pdf rule)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                Page page = pdfDoc.Pages[pageNumber];

                // Build the output file name for each page
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");

                // Create a PNG device with desired resolution and a transparent background.
                // Resolution is expressed in DPI.
                var resolution = new Resolution(300);
                var pngDevice = new PngDevice(resolution);
                pngDevice.TransparentBackground = true; // enable transparent background

                // Convert the current page to PNG using the PNG device.
                pngDevice.Process(page, outputPath);

                Console.WriteLine($"Saved page {pageNumber} as PNG: {outputPath}");
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
