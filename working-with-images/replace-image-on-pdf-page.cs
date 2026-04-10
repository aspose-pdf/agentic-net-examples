using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImageExample
{
    static void Main()
    {
        // Paths for the source PDF, the new image, and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string newImagePath  = "newImage.jpg";
        const string outputPdfPath = "output.pdf";

        // Ensure the source files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(sourcePdfPath))
        {
            // Select the page that contains the image to replace (first page in this example)
            Page page = pdfDoc.Pages[1];

            // Access the collection of images (XImage objects) on the page
            var images = page.Resources.Images;

            // Verify that the page actually contains at least one image
            if (images.Count == 0)
            {
                Console.WriteLine("No images found on the selected page.");
            }
            else
            {
                // Replace the first image (index is 1‑based) with the new image stream
                using (FileStream newImgStream = File.OpenRead(newImagePath))
                {
                    // Overload Replace(int index, Stream stream) replaces the image at the given index
                    images.Replace(1, newImgStream);
                }

                Console.WriteLine("Image replaced successfully.");
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
    }
}