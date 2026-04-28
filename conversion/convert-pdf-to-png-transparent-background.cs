using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        const string pdfFile = @"YOUR_PDF_FILE";

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Define the resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the resolution and enable transparent background
            PngDevice pngDevice = new PngDevice(resolution)
            {
                TransparentBackground = true
            };

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(dataDir, $"image{pageNumber}_out.png");

                // Convert the current page to PNG and save it
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Saved page {pageNumber} to {outputPath}");
            }
        }
    }
}