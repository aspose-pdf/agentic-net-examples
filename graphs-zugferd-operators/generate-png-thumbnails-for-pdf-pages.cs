using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Path to the directory containing the PDF file
        string dataDir = @"YOUR_DATA_DIRECTORY";
        // Input PDF file name
        string pdfFile = "input.pdf";

        // Combine directory and file name to get full path
        string pdfPath = Path.Combine(dataDir, pdfFile);

        // Ensure the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create a ThumbnailDevice with custom width and height (e.g., 150x150 pixels)
            ThumbnailDevice thumbnailDevice = new ThumbnailDevice(150, 150);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Define the output PNG file name for the current page thumbnail
                string thumbPath = Path.Combine(dataDir, $"thumb_page{pageNumber}.png");

                // Create the output file stream inside a using block
                using (FileStream outputStream = new FileStream(thumbPath, FileMode.Create))
                {
                    // Convert the current page to a PNG thumbnail and write it to the stream
                    thumbnailDevice.Process(pdfDocument.Pages[pageNumber], outputStream);
                }

                Console.WriteLine($"Thumbnail saved: {thumbPath}");
            }
        }
    }
}