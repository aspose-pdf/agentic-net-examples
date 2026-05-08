using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (no Facades)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string newImagePath   = "newImage.jpg";

        // Page and image indexes are 1‑based in Aspose.Pdf
        const int pageNumber = 1;   // page where the image will be replaced
        const int imageIndex = 1;   // index of the image to replace on that page

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Access the page's image resources collection
            Page page = pdfDoc.Pages[pageNumber];
            var images = page.Resources.Images;

            // Validate the image index
            if (imageIndex < 1 || imageIndex > images.Count)
            {
                Console.Error.WriteLine($"Image index {imageIndex} is out of range. Page contains {images.Count} images.");
                return;
            }

            // Open the new image as a stream and replace the existing image
            using (FileStream imgStream = new FileStream(newImagePath, FileMode.Open, FileAccess.Read))
            {
                // Replace the image at the specified index with the new image stream
                images.Replace(imageIndex, imgStream);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image on page {pageNumber} (index {imageIndex}) replaced successfully.");
        Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
    }
}