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
        const int targetPageNumber = 2; // page to modify (1‑based)

        // Map of image index (1‑based) on the page to the new image file path
        var replacements = new Dictionary<int, string>
        {
            { 1, "newImage1.jpg" },
            { 3, "newImage3.png" }
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        foreach (var path in replacements.Values)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Replacement image not found: {path}");
                return;
            }
        }

        using (Document doc = new Document(inputPath))
        {
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];
            var images = page.Resources.Images; // XImageCollection

            foreach (var kvp in replacements)
            {
                int index = kvp.Key;
                string newImagePath = kvp.Value;

                if (index < 1 || index > images.Count)
                {
                    Console.Error.WriteLine($"Image index {index} out of range on page {targetPageNumber}.");
                    continue;
                }

                using (FileStream fs = File.OpenRead(newImagePath))
                {
                    images.Replace(index, fs);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Images replaced and saved to '{outputPath}'.");
    }
}