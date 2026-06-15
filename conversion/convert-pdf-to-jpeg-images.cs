using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // JpegDevice resides here

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory for JPEG images
        const string outputDir = "JpegImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // JpegDevice with default resolution and maximum quality
            JpegDevice jpegDevice = new JpegDevice();

            // Iterate pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build output file name for each page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                // Convert page to JPEG using a FileStream inside a using block
                using (FileStream jpegStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG: {outputPath}");
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}