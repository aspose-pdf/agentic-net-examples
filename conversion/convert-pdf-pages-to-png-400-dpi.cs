using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Output folder for PNG images
        const string outputFolder = "PageImages";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create the output directory if it does not exist
        Directory.CreateDirectory(outputFolder);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define the resolution (400 DPI)
            Resolution resolution = new Resolution(400);

            // Create a PNG device that will render pages at the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Build the output file name (e.g., Page_1.png)
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");

                // Render the specific page to PNG and save it directly to the file system
                pngDevice.Process(pdfDoc.Pages[pageNumber], outputPath);

                Console.WriteLine($"Saved page {pageNumber} as PNG to: {outputPath}");
            }
        }

        Console.WriteLine("All pages have been exported as PNG images.");
    }
}