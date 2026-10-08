using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths to the source PDF portfolio and the thumbnail image.
        const string portfolioPath = "portfolio.pdf";
        const string thumbnailPath = "thumb.jpg";
        const string outputPath    = "portfolio_with_thumbnails.pdf";

        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio PDF not found: {portfolioPath}");
            return;
        }

        if (!File.Exists(thumbnailPath))
        {
            Console.Error.WriteLine($"Thumbnail image not found: {thumbnailPath}");
            return;
        }

        // Load the PDF portfolio inside a using block for deterministic disposal.
        using (Document doc = new Document(portfolioPath))
        {
            // Ensure the document contains embedded files (portfolio items).
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF portfolio.");
            }
            else
            {
                // Load the thumbnail image once; reuse it for each embedded file.
                using (FileStream thumbStream = File.OpenRead(thumbnailPath))
                {
                    // Create an Aspose.Pdf.Drawing.Image from the stream.
                    Image thumbnailImage = new Image { ImageStream = thumbStream };

                    // NOTE: In the current Aspose.Pdf version the FileSpecification class does not expose an
                    // EmbeddedFile property, nor the EmbeddedFile.Thumbnail property used in older examples.
                    // Therefore we cannot assign a thumbnail directly to the embedded file via the API.
                    // The code below demonstrates the intended iteration and leaves a placeholder where a
                    // future version that supports thumbnails could be hooked in.
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        // Placeholder for thumbnail assignment – not supported in this API version.
                        // Example (if supported in a later version):
                        // fileSpec.EmbeddedFile.Thumbnail = thumbnailImage;
                    }

                    Console.WriteLine($"Processed {doc.EmbeddedFiles.Count} portfolio items (thumbnail assignment not supported in this API version).");

                    // Save the modified PDF while the thumbnail stream is still open.
                    doc.Save(outputPath);
                }
            }
        }

        Console.WriteLine($"Portfolio saved to '{outputPath}'.");
    }
}
