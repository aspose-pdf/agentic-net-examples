using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Page number where images will be replaced (1‑based indexing)
        const int targetPageNumber = 2;

        // Dictionary mapping image index (1‑based) to the new image file path
        var imageReplacements = new Dictionary<int, string>
        {
            { 1, "newImage1.jpg" },
            { 3, "newImage3.png" }
        };

        // Validate input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Validate all replacement image files exist
        foreach (var kvp in imageReplacements)
        {
            if (!File.Exists(kvp.Value))
            {
                Console.Error.WriteLine($"Replacement image not found: {kvp.Value}");
                return;
            }
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the requested page exists (pages are 1‑based)
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page and its image collection
            Page page = doc.Pages[targetPageNumber];
            XImageCollection images = page.Resources.Images;

            // Iterate over the replacement map
            foreach (var kvp in imageReplacements)
            {
                int index = kvp.Key;          // image index in the collection (1‑based)
                string newImagePath = kvp.Value;

                // Verify the index is within the collection bounds
                if (index < 1 || index > images.Count)
                {
                    Console.Error.WriteLine($"Image index {index} is out of range (1..{images.Count}). Skipping.");
                    continue;
                }

                // Replace the image at the specified index with the new image stream
                using (FileStream fs = File.OpenRead(newImagePath))
                {
                    images.Replace(index, fs);
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image replacement completed. Saved to '{outputPdf}'.");
    }
}