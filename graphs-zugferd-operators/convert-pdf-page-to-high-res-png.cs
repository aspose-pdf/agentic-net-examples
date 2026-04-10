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
        // Output PNG file path (first page)
        const string outputPngPath = "page1.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a Resolution object for 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialize PngDevice with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = pdfDocument.Pages[1];

            // Convert the page to PNG and save it (lifecycle: save)
            using (FileStream pngStream = new FileStream(outputPngPath, FileMode.Create))
            {
                pngDevice.Process(page, pngStream);
            }
        }

        Console.WriteLine($"Page 1 saved as PNG at '{outputPngPath}'.");
    }
}