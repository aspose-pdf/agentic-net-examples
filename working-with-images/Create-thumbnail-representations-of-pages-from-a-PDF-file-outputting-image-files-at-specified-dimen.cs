using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Directory where thumbnail images will be saved
        const string outputDir = "Thumbnails";

        // Desired thumbnail dimensions (in pixels)
        const int thumbWidth = 200;
        const int thumbHeight = 200;

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Create a ThumbnailDevice with the required dimensions
                ThumbnailDevice thumbDevice = new ThumbnailDevice(thumbWidth, thumbHeight);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build output file name for the current page
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}_thumb.png");

                    // Process the page and write the thumbnail PNG to a file stream
                    using (FileStream outputStream = new FileStream(outputPath, FileMode.Create))
                    {
                        thumbDevice.Process(pdfDocument.Pages[pageNumber], outputStream);
                    }

                    Console.WriteLine($"Thumbnail saved: {outputPath}");
                }
            }

            Console.WriteLine("All thumbnails generated successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}