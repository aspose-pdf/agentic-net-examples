using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Folder where BMP images will be saved
        const string outputFolder = "output";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a Resolution object with 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialize the BMP device with the specified resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // OPTIONAL: set custom rendering options for higher quality output
            // (properties may vary depending on the Aspose.Pdf version)
            bmpDevice.RenderingOptions = new RenderingOptions
            {
                // Example: enable anti‑aliasing for smoother graphics
                // AntiAliasing = true
            };

            // Iterate over all pages (Aspose.Pdf uses 1‑based page indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");

                // Convert each page to BMP and write it to a file stream
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as BMP to '{outputPath}'.");
            }
        }
    }
}