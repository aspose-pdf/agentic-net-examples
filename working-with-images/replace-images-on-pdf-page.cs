using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPage = 2; // 1‑based page number

        // Map of original image index (1‑based) -> replacement image file path
        var replacements = new Dictionary<int, string>
        {
            { 1, "newImage1.jpg" },
            { 3, "newImage3.png" }
        };

        // Validate input PDF
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Validate replacement images
        foreach (var kvp in replacements)
        {
            if (!File.Exists(kvp.Value))
            {
                Console.Error.WriteLine($"Replacement image not found: {kvp.Value}");
                return;
            }
        }

        // Load document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists (pages are 1‑based)
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number: {targetPage}");
                return;
            }

            Page page = doc.Pages[targetPage];

            // Replace each image on the page using the XImageCollection.Replace method
            foreach (var kvp in replacements)
            {
                int imgIndex = kvp.Key;          // 1‑based index of the image to replace
                string newImgPath = kvp.Value;   // path to the new image file

                // Validate that the image index exists on the page
                if (imgIndex < 1 || imgIndex > page.Resources.Images.Count)
                {
                    Console.Error.WriteLine($"Image index {imgIndex} out of range on page {targetPage}.");
                    continue;
                }

                // Replace the image stream
                using (FileStream fs = File.OpenRead(newImgPath))
                {
                    // XImageCollection.Replace replaces the image at the given 1‑based index
                    page.Resources.Images.Replace(imgIndex, fs);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Images replaced successfully. Output saved to '{outputPath}'.");
    }
}
