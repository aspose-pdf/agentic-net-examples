using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

namespace AsposePdfApi
{
    class Program
    {
        static void Main()
        {
            // Input PDF file path
            const string inputPdfPath = "input.pdf";
            // Output directory for PNG images
            const string outputDir = "output_images";

            // Ensure output directory exists
            Directory.CreateDirectory(outputDir);

            // If the input PDF does not exist, create a simple one so the example can run self‑contained
            if (!File.Exists(inputPdfPath))
            {
                using (Document tempDoc = new Document())
                {
                    // Add a blank page (or you can add any content you like)
                    tempDoc.Pages.Add();
                    tempDoc.Save(inputPdfPath);
                    Console.WriteLine($"Sample PDF created at '{inputPdfPath}'.");
                }
            }

            // Load the PDF document inside a using block for proper disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Define desired resolution (e.g., 300 DPI)
                Resolution resolution = new Resolution(300);

                // Create a PngDevice with the specified resolution and enable transparent background
                PngDevice pngDevice = new PngDevice(resolution)
                {
                    TransparentBackground = true
                };

                // Iterate through all pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build output file name for each page
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                    // Process the page and write the PNG to a file stream
                    using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                    {
                        pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                    }

                    Console.WriteLine($"Saved page {pageNumber} as PNG to '{outputPath}'.");
                }
            }
        }
    }
}
