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
        // Output JPEG file path (for the selected page)
        const string outputJpegPath = "page1.jpg";
        // Page number to convert (Aspose.Pdf uses 1‑based indexing)
        const int pageNumber = 1;

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Validate the requested page number
            if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // JpegDevice with default resolution (150 DPI) and maximum quality
            JpegDevice jpegDevice = new JpegDevice();

            // Create a file stream for the JPEG output
            using (FileStream jpegStream = new FileStream(outputJpegPath, FileMode.Create))
            {
                // Convert the specified page to JPEG and write to the stream
                jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
            }
        }

        Console.WriteLine($"Page {pageNumber} saved as JPEG to '{outputJpegPath}'.");
    }
}