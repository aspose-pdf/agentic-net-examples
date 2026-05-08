using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, the page containing the image, and the new image file.
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string newImagePath  = "newImage.jpg";

        // Page number (1‑based) where the image to replace is located.
        const int pageNumber = 1;

        // Index of the image in the page's image collection (1‑based).
        // Adjust this value if the target image is not the first one.
        const int imageIndex = 1;

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

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Validate that the requested page exists.
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Validate that the requested image index exists on the page.
            Page srcPage = pdfDoc.Pages[pageNumber];
            int imagesCount = srcPage.Resources.Images.Count; // 1‑based collection
            if (imageIndex < 1 || imageIndex > imagesCount)
            {
                Console.Error.WriteLine($"Image index {imageIndex} is out of range. Page {pageNumber} contains {imagesCount} image(s).");
                return;
            }

            // Replace the image resource with the new image stream.
            // The Replace method keeps the original placement (rectangle, transformation matrix, etc.).
            using (FileStream imgStream = File.OpenRead(newImagePath))
            {
                srcPage.Resources.Images.Replace(imageIndex, imgStream);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image on page {pageNumber} (index {imageIndex}) replaced successfully. Saved to '{outputPdfPath}'.");
    }
}