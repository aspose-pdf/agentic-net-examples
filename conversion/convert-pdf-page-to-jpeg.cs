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

        // Page number to convert (1‑based indexing)
        const int pageNumber = 1;

        // Output JPEG file path
        const string jpegPath = "page1.jpg";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Validate the requested page number
            if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {pdfDocument.Pages.Count} pages.");
                return;
            }

            // Create a JpegDevice with default resolution (150 DPI) and maximum quality
            JpegDevice jpegDevice = new JpegDevice();

            // Get the specific page (1‑based indexing)
            Page page = pdfDocument.Pages[pageNumber];

            // Save the page as a JPEG image using a FileStream
            using (FileStream jpegStream = new FileStream(jpegPath, FileMode.Create))
            {
                jpegDevice.Process(page, jpegStream);
            }
        }

        Console.WriteLine($"Page {pageNumber} of '{pdfPath}' saved as JPEG to '{jpegPath}'.");
    }
}