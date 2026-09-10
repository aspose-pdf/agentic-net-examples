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
        // Page number to convert (1‑based indexing)
        const int pageNumber = 2;
        // Output JPEG file path
        const string outputJpegPath = "page2.jpg";

        // Verify that the source PDF exists
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

            // Create a JpegDevice with default resolution (150 DPI) and maximum quality
            JpegDevice jpegDevice = new JpegDevice();

            // Open a file stream for the JPEG output
            using (FileStream jpegStream = new FileStream(outputJpegPath, FileMode.Create))
            {
                // Convert the specified page to JPEG and write to the stream
                jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
            }
        }

        Console.WriteLine($"Page {pageNumber} saved as JPEG to '{outputJpegPath}'.");
    }
}