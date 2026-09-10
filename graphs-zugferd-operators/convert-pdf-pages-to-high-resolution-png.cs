using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF file
        string dataDir = @"C:\Data\";
        // PDF file name
        string pdfFile = "sample.pdf";

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create a Resolution object for 300 DPI
            Resolution resolution = new Resolution(300);
            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Output PNG file path for the current page
                string outPath = Path.Combine(dataDir, $"page{pageNumber}.png");

                // Convert the page to PNG and write to a file stream
                using (FileStream pngStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as PNG to: {outPath}");
            }
        }
    }
}