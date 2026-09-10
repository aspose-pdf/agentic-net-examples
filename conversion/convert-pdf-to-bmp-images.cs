using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a BmpDevice with default settings (default resolution)
            BmpDevice bmpDevice = new BmpDevice();

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output BMP file path for the current page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                // Open a FileStream for the BMP output; the using block ensures the stream is closed
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to BMP and write it to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF has been successfully converted to BMP images.");
    }
}