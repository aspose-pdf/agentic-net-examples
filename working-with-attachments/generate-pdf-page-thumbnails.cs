using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // ThumbnailDevice resides here

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";          // source PDF containing portfolio items
        const string outputPath = "portfolio_with_thumbs.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Create a new PDF that will hold the thumbnails
            Document thumbDoc = new Document();

            // Iterate over each page (portfolio item) in the source PDF
            foreach (Page srcPage in srcDoc.Pages)
            {
                // Create a thumbnail device with desired size (e.g., 150x150 pixels)
                ThumbnailDevice thumbDevice = new ThumbnailDevice(150, 150);

                // Render the page to a PNG image stored in a memory stream
                using (MemoryStream thumbStream = new MemoryStream())
                {
                    thumbDevice.Process(srcPage, thumbStream);
                    thumbStream.Position = 0; // reset stream for reading

                    // Add a new page to the thumbnail document
                    Page thumbPage = thumbDoc.Pages.Add();

                    // Define the rectangle where the thumbnail image will be placed
                    // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 150, 150);

                    // Add the thumbnail image to the new page
                    thumbPage.AddImage(thumbStream, rect);
                }
            }

            // Save the document containing all thumbnails
            thumbDoc.Save(outputPath);
        }

        Console.WriteLine($"Thumbnails generated and saved to '{outputPath}'.");
    }
}