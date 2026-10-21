using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExportedPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Set the desired resolution (200 DPI)
            Resolution resolution = new Resolution(200);

            // Create a PNG device with transparent background
            PngDevice pngDevice = new PngDevice(resolution)
            {
                TransparentBackground = true // <-- correct property for transparency
            };

            // Convert pages 2 through 4 (inclusive)
            for (int pageNumber = 2; pageNumber <= 4; pageNumber++)
            {
                // Ensure the page exists in the source document
                if (pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNumber} does not exist. Skipping.");
                    continue;
                }

                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Render the specific page to PNG using the configured device
                pngDevice.Process(doc.Pages[pageNumber], outputPath);
                Console.WriteLine($"Page {pageNumber} saved as {outputPath}");
            }
        }
    }
}
