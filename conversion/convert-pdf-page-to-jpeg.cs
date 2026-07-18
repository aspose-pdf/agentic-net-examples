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

        // Output JPEG file path (will contain the image of the selected page)
        const string outputJpegPath = "page1.jpg";

        // Page number to convert (Aspose.Pdf uses 1‑based indexing)
        const int pageNumber = 1;

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {pdfDocument.Pages.Count} pages.");
                return;
            }

            // Retrieve the specific page (1‑based index)
            Page page = pdfDocument.Pages[pageNumber];

            // Create a JpegDevice with default resolution (150 DPI) and maximum quality
            JpegDevice jpegDevice = new JpegDevice();

            // Open a file stream for the output JPEG image
            using (FileStream jpegStream = new FileStream(outputJpegPath, FileMode.Create))
            {
                // Convert the selected PDF page to JPEG and write it to the stream
                jpegDevice.Process(page, jpegStream);
                // The using statement will automatically close the stream
            }
        }

        Console.WriteLine($"Page {pageNumber} of '{inputPdfPath}' saved as JPEG to '{outputJpegPath}'.");
    }
}