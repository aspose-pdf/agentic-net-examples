using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output directory for JPEG images
        const string outputDir = "JpegImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // JpegDevice with default resolution (150 DPI) and maximum quality
            JpegDevice jpegDevice = new JpegDevice();

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build output file name for each page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                // Create a file stream for the JPEG image
                using (FileStream jpegStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to JPEG and write to the stream
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG: {outputPath}");
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}