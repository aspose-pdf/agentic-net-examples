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
        // Output JPEG file path
        const string outputJpegPath = "page1_output.jpeg";
        // Page number to convert (1‑based indexing)
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
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {pdfDocument.Pages.Count} pages.");
                return;
            }

            // Create a JpegDevice with default resolution (150 DPI) and maximum quality
            JpegDevice jpegDevice = new JpegDevice();

            // Convert the specified page to JPEG and write to a file stream
            using (FileStream jpegStream = new FileStream(outputJpegPath, FileMode.Create))
            {
                jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                // Stream will be closed automatically by the using statement
            }
        }

        Console.WriteLine($"Page {pageNumber} saved as JPEG to '{outputJpegPath}'.");
    }
}