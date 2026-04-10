using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImageExample
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string newImagePath  = "newImage.jpg";   // replacement image (JPEG)
        const string outputPdfPath = "output.pdf";     // result PDF
        const int    pageNumber    = 1;                // page to modify (1‑based)

        // Ensure files exist
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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the target page
            Page page = doc.Pages[pageNumber];

            // Access the image collection of the page
            XImageCollection images = page.Resources.Images;

            // Verify that the page contains at least one image
            if (images.Count == 0)
            {
                Console.WriteLine("No images found on the specified page.");
                doc.Save(outputPdfPath); // save unchanged document
                return;
            }

            // Replace the first image (index is 1‑based) with the new image stream
            using (FileStream newImgStream = File.OpenRead(newImagePath))
            {
                // Preserve layout because the placement references the same resource index
                images.Replace(1, newImgStream);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image replaced and saved to '{outputPdfPath}'.");
    }
}