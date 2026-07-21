using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // ThumbnailDevice resides in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Directory where thumbnail PNGs will be saved
        const string outputDirectory = "Thumbnails";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a ThumbnailDevice with the required size (150x200 pixels)
            ThumbnailDevice thumbnailDevice = new ThumbnailDevice(150, 200);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputDirectory, $"page_{pageNumber}.png");

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