using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPageNumber = 2; // 1‑based page number to modify

        // Map original image index (1‑based) to the file path of the replacement image
        var imageReplacements = new Dictionary<int, string>
        {
            { 1, "newImage1.jpg" },
            { 3, "newImage3.png" }
        };

        // Verify input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Verify each replacement image exists
        foreach (var kvp in imageReplacements)
        {
            if (!File.Exists(kvp.Value))
            {
                Console.Error.WriteLine($"Replacement image not found: {kvp.Value}");
                return;
            }
        }

        // Load PDF inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate requested page number (pages are 1‑based)
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];

            // Replace each specified image on the page
            foreach (var kvp in imageReplacements)
            {
                int imgIndex = kvp.Key;          // 1‑based index within the page's image collection
                string newImgPath = kvp.Value;

                // Ensure the image index exists on this page
                if (imgIndex < 1 || imgIndex > page.Resources.Images.Count)
                {
                    Console.Error.WriteLine($"Image index {imgIndex} out of range on page {targetPageNumber}.");
                    continue;
                }

                // Replace the existing image at the specified index with the new image stream
                using (FileStream imgStream = File.OpenRead(newImgPath))
                {
                    // The Replace overload expects a Stream, not an XImage
                    page.Resources.Images.Replace(imgIndex, imgStream);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Images replaced and saved to '{outputPath}'.");
    }
}
