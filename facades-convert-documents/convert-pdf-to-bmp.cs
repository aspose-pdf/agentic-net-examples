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
        // Directory where BMP images will be saved
        const string outputDir = "output_images";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a Resolution object with 200 DPI
            Resolution resolution = new Resolution(200);

            // Initialize BmpDevice with the specified resolution.
            // CropBox is used by default, so no extra configuration is needed.
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Pages collection is 1‑based; iterate through all pages
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output BMP file name
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                // Convert the current page to BMP and write it to a file stream
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Saved BMP image: {outputPath}");
            }
        }
    }
}