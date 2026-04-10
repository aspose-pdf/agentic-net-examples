using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where thumbnail PNGs will be saved
        const string outputDir = "Thumbnails";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block (document-disposal-with-using rule)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a ThumbnailDevice with custom size (e.g., 150x150 pixels)
            // ThumbnailDevice(int width, int height) constructor is used here.
            ThumbnailDevice thumbnailDevice = new ThumbnailDevice(150, 150);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputDir, $"thumb_page{pageNumber}.png");

                // Process the page and write the PNG thumbnail to a file stream
                using (FileStream outputStream = new FileStream(outputPath, FileMode.Create))
                {
                    thumbnailDevice.Process(pdfDocument.Pages[pageNumber], outputStream);
                }
            }
        }

        Console.WriteLine("Thumbnail images have been generated successfully.");
    }
}