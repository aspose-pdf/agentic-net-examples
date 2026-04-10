using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory for PNG images
        const string outputDir = "OutputImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Check that the document has at least 4 pages
            if (pdfDocument.Pages.Count < 4)
            {
                Console.Error.WriteLine("Error: The PDF does not contain pages 2‑4.");
                return;
            }

            // Create a resolution of 200 DPI
            Resolution resolution200Dpi = new Resolution(200);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution200Dpi)
            {
                // Enable transparent background for the generated PNGs
                TransparentBackground = true
            };

            // Convert pages 2 through 4
            for (int pageNumber = 2; pageNumber <= 4; pageNumber++)
            {
                // Build the output file name (e.g., "Page_2.png")
                string outputFilePath = Path.Combine(outputDir, $"Page_{pageNumber}.png");

                // Open a file stream for writing the PNG image
                using (FileStream pngStream = new FileStream(outputFilePath, FileMode.Create))
                {
                    // Process the specific page and write the PNG to the stream
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as PNG → {outputFilePath}");
            }
        }

        Console.WriteLine("Selected pages have been successfully converted to PNG images.");
    }
}