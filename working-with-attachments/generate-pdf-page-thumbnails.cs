using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // ThumbnailDevice resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "portfolio.pdf";          // source PDF containing portfolio items
        const string outputFolder = "thumbnails";         // folder to store generated thumbnails

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle: create -> load -> save)
        using (Document doc = new Document(inputPdf))
        {
            // Create a thumbnail device with desired dimensions (e.g., 150x150 pixels)
            ThumbnailDevice thumbDevice = new ThumbnailDevice(150, 150);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the output path for the thumbnail of the current page
                string thumbPath = Path.Combine(outputFolder, $"thumb_page_{i}.png");

                // Generate the thumbnail and write it to a PNG file
                using (FileStream outStream = new FileStream(thumbPath, FileMode.Create))
                {
                    thumbDevice.Process(page, outStream);
                }
            }
        }

        Console.WriteLine("Thumbnail images have been created for each portfolio item.");
    }
}