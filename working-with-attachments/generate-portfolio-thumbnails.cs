using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;          // ThumbnailDevice
using Aspose.Pdf.Drawing;          // Graph, Rectangle, etc.

class PortfolioThumbnailGenerator
{
    static void Main()
    {
        const string inputPdfPath = "portfolio.pdf";          // source PDF with portfolio items
        const string outputPdfPath = "portfolio_with_thumbs.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Create a new PDF that will contain the thumbnails
            using (Document thumbDoc = new Document())
            {
                // Iterate through each page of the source PDF (1‑based indexing)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    Page srcPage = srcDoc.Pages[i];

                    // Generate a thumbnail (default 200x200) for the current page
                    using (MemoryStream thumbStream = new MemoryStream())
                    {
                        // ThumbnailDevice writes a PNG image to the provided stream
                        ThumbnailDevice thumbDevice = new ThumbnailDevice();
                        thumbDevice.Process(srcPage, thumbStream);
                        thumbStream.Position = 0; // reset for reading

                        // Add a new page to the thumbnail document
                        Page thumbPage = thumbDoc.Pages.Add();

                        // Define the rectangle where the thumbnail will be placed
                        // (centered on the page, preserving aspect ratio)
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                            50,                     // lower‑left X
                            500,                    // lower‑left Y
                            550,                    // upper‑right X
                            750);                   // upper‑right Y

                        // Place the thumbnail image onto the new page
                        thumbPage.AddImage(thumbStream, rect);
                    }
                }

                // Save the document containing all thumbnails
                thumbDoc.Save(outputPdfPath);
                Console.WriteLine($"Thumbnail PDF saved to '{outputPdfPath}'.");
            }
        }
    }
}