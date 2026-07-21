using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // ThumbnailDevice resides here

public class PortfolioThumbnailGenerator
{
    public static void Main()
    {
        const string inputPdf = "portfolio.pdf";
        const string outputPdf = "portfolio_with_thumbnails.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF (portfolio)
        using (Document srcDoc = new Document(inputPdf))
        {
            // Create a new document that will contain the thumbnails
            using (Document resultDoc = new Document())
            {
                // Iterate over each page (each portfolio item)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    Page srcPage = srcDoc.Pages[i];

                    // Generate a thumbnail (150x150 pixels) for the current page
                    using (MemoryStream thumbStream = new MemoryStream())
                    {
                        ThumbnailDevice thumbDevice = new ThumbnailDevice(150, 150);
                        thumbDevice.Process(srcPage, thumbStream);
                        thumbStream.Position = 0; // Reset stream position for reading

                        // Add a new page to the result document
                        Page thumbPage = resultDoc.Pages.Add();

                        // Define the rectangle where the thumbnail will be placed
                        // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 150, 150);

                        // Insert the thumbnail image onto the new page
                        thumbPage.AddImage(thumbStream, rect);
                    }
                }

                // Save the document containing all thumbnails
                resultDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Thumbnails generated and saved to '{outputPdf}'.");
    }
}