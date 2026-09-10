using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF file
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Full path to the source PDF
        string pdfPath = Path.Combine(dataDir, "YOUR_PDF_FILE.pdf");

        // Verify that the PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // JpegDevice with default resolution and maximum quality
            JpegDevice jpegDevice = new JpegDevice();

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Output file name for each page
                string outputPath = Path.Combine(dataDir, $"image{pageNumber}_out.jpeg");

                // Create a file stream for the JPEG image
                using (FileStream jpegStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to JPEG and write to the stream
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as {outputPath}");
            }
        }
    }
}