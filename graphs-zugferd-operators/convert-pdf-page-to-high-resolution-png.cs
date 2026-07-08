using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output PNG file path (first page will be saved)
        const string pngPath = "page1.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Create a Resolution object with 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Select the page to convert (1‑based indexing)
            Page page = pdfDocument.Pages[1];

            // Convert the page to PNG and write to a file stream
            using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
            {
                pngDevice.Process(page, pngStream);
            }

            Console.WriteLine($"Page 1 saved as PNG with 300 DPI to '{pngPath}'.");
        }
    }
}