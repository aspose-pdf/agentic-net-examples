using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesOnPage
{
    static void Main()
    {
        // Input PDF, output PDF and the page number to process
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const int    pageIndex = 2;               // 1‑based page number

        // Mapping of original image indices (1‑based) to new image file paths
        var imageReplacements = new Dictionary<int, string>
        {
            { 1, "newImage1.jpg" },
            { 3, "newImage3.png" }
        };

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Process the document
        using (Document doc = new Document(inputPdf))
        {
            // Validate page number
            if (pageIndex < 1 || pageIndex > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page number {pageIndex} is out of range.");
                return;
            }

            // Get the target page and its image collection
            Page page = doc.Pages[pageIndex];
            XImageCollection images = page.Resources.Images;

            // Replace each specified image
            foreach (var kvp in imageReplacements)
            {
                int imageIndex = kvp.Key;          // 1‑based index in the collection
                string newImagePath = kvp.Value;

                if (!File.Exists(newImagePath))
                {
                    Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
                    continue; // skip missing files
                }

                // Ensure the index is within the collection bounds
                if (imageIndex < 1 || imageIndex > images.Count)
                {
                    Console.Error.WriteLine($"Image index {imageIndex} is out of range (1..{images.Count}).");
                    continue;
                }

                // Replace the image using a stream (JPEG format is expected)
                using (FileStream imgStream = File.OpenRead(newImagePath))
                {
                    images.Replace(imageIndex, imgStream);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Images replaced and saved to '{outputPdf}'.");
    }
}