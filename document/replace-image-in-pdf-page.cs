using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImageInPdf
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string newImagePath   = "newImage.jpg";
        const int    targetPageNum  = 1;   // 1‑based page index
        const int    imageIndex     = 1;   // 1‑based index of the image in the page's resources

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"New image not found: {newImagePath}");
            return;
        }

        // Load the PDF, modify, and save – all within using for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (targetPageNum < 1 || targetPageNum > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNum} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Get the target page
            Page page = pdfDoc.Pages[targetPageNum];

            // Access the image collection for this page
            XImageCollection images = page.Resources.Images;

            // Validate the image index
            if (imageIndex < 1 || imageIndex > images.Count)
            {
                Console.Error.WriteLine($"Image index {imageIndex} is out of range. Page contains {images.Count} images.");
                return;
            }

            // Replace the image at the specified index with the new image stream
            using (FileStream newImgStream = File.OpenRead(newImagePath))
            {
                // XImageCollection.Replace keeps the original resource reference,
                // so any existing placements on the page retain their layout.
                images.Replace(imageIndex, newImgStream);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image replaced successfully. Output saved to '{outputPdfPath}'.");
    }
}