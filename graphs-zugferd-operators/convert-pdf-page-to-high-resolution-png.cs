using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // PngDevice and Resolution classes

class PdfToPngConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output PNG file path (first page only)
        const string pngPath = "page1.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create a Resolution object with 300 DPI for high‑quality output
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = pdfDocument.Pages[1];

            // Convert the page to PNG and write it to a file stream
            using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
            {
                pngDevice.Process(page, pngStream);
            }

            Console.WriteLine($"Page 1 saved as PNG to '{pngPath}' with 300 DPI.");
        }
    }
}