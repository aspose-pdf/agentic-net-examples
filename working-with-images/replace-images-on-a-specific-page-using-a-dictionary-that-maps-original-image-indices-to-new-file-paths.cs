using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesOnPage
{
    static void Main()
    {
        // Input PDF, output PDF and the page number to modify
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const int targetPageNumber = 2; // 1‑based page index

        // Mapping of image index (1‑based) to the new image file path
        var imageReplacements = new Dictionary<int, string>
        {
            { 1, "newImage1.jpg" },
            { 3, "newImage3.png" }
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Validate the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[targetPageNumber];

            // Access the image collection of the page
            XImageCollection images = page.Resources.Images;

            // Iterate over the replacement dictionary
            foreach (KeyValuePair<int, string> kvp in imageReplacements)
            {
                int index = kvp.Key;          // Image index in the collection (1‑based)
                string newImagePath = kvp.Value;

                if (index < 1 || index > images.Count)
                {
                    Console.Error.WriteLine($"Image index {index} is out of range. Page has {images.Count} images.");
                    continue; // skip invalid index
                }

                if (!File.Exists(newImagePath))
                {
                    Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
                    continue; // skip missing file
                }

                // Replace the image at the specified index with the new image stream
                using (FileStream newImageStream = File.OpenRead(newImagePath))
                {
                    images.Replace(index, newImageStream);
                }
            }

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images replaced and saved to '{outputPdfPath}'.");
    }
}