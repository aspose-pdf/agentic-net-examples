using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithQrPlaceholder
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string placeholderImagePath = "qr_placeholder.png"; // PNG containing a QR code

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(placeholderImagePath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImagePath}");
            return;
        }

        // Load the PDF document (load rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                XImageCollection images = page.Resources.Images;

                // XImageCollection is 1‑based; replace each image with the QR placeholder
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Open the placeholder image stream
                    using (FileStream placeholderStream = File.OpenRead(placeholderImagePath))
                    {
                        // Replace the existing image at the current index
                        // XImageCollection.Replace(int, Stream) follows the documented signature
                        images.Replace(imgIndex, placeholderStream);
                    }
                }
            }

            // Save the modified PDF (save rule)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with QR placeholders: {outputPdfPath}");
    }
}