using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Page number to modify (1‑based indexing)
        const int pageNumber = 1;

        // Dictionary mapping image index (1‑based) to new image file path
        var imageReplacements = new Dictionary<int, string>
        {
            { 1, "newImage1.jpg" },
            { 2, "newImage2.png" }
        };

        // Validate input PDF
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        // Validate replacement image files
        foreach (var kvp in imageReplacements)
        {
            if (!File.Exists(kvp.Value))
            {
                Console.Error.WriteLine($"Replacement image not found: {kvp.Value}");
                return;
            }
        }

        // Load PDF document (using rule: wrap Document in using)
        using (Document doc = new Document(inputPath))
        {
            // Validate requested page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page number {pageNumber} is out of range.");
                return;
            }

            // Get the target page
            Page page = doc.Pages[pageNumber];

            // Replace each specified image
            foreach (var kvp in imageReplacements)
            {
                int imgIndex = kvp.Key;          // 1‑based index in the XImageCollection
                string newImgPath = kvp.Value;   // Path to the new image file

                // Ensure the image index exists on this page
                if (imgIndex < 1 || imgIndex > page.Resources.Images.Count)
                {
                    Console.Error.WriteLine($"Image index {imgIndex} is out of range on page {pageNumber}.");
                    continue;
                }

                // Open the new image stream and replace the existing image
                using (FileStream imgStream = File.OpenRead(newImgPath))
                {
                    page.Resources.Images.Replace(imgIndex, imgStream);
                }
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Images replaced successfully. Output saved to '{outputPath}'.");
    }
}