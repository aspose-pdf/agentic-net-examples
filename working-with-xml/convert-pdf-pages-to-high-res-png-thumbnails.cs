using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Thumbnails";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define a high‑resolution (e.g., 300 DPI) for the PNG images
            Resolution highRes = new Resolution(300);

            // Create a PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(highRes);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string pngPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Save the page as a PNG image using a FileStream
                using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("All pages have been converted to high‑resolution PNG thumbnails.");
    }
}