using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // ThumbnailDevice resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Thumbnails";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a ThumbnailDevice with the required dimensions (150x200 pixels)
            ThumbnailDevice thumbnailDevice = new ThumbnailDevice(150, 200);

            // Pages are 1‑based indexed in Aspose.Pdf
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string thumbnailPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Create a file stream for the PNG thumbnail
                using (FileStream outputStream = new FileStream(thumbnailPath, FileMode.Create))
                {
                    // Convert the current page to a PNG thumbnail and write to the stream
                    thumbnailDevice.Process(pdfDocument.Pages[pageNumber], outputStream);
                }

                Console.WriteLine($"Thumbnail saved: {thumbnailPath}");
            }
        }
    }
}