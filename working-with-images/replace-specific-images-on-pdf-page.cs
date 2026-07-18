using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        int targetPageNumber = 2; // 1‑based page index to modify

        // Map of image index (1‑based) to the file path of the replacement image
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

        // Validate replacement image files
        foreach (var kvp in replacements)
        {
            if (!File.Exists(kvp.Value))
            {
                Console.Error.WriteLine($"Replacement image not found: {kvp.Value}");
                return;
            }
        }

        try
        {
            // Load the PDF (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Ensure the requested page exists (pages are 1‑based)
                if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine("Invalid page number.");
                    return;
                }

                Page page = doc.Pages[targetPageNumber];
                XImageCollection images = page.Resources.Images;

                // Iterate over the dictionary and replace each specified image
                foreach (var kvp in replacements)
                {
                    int index = kvp.Key;               // Image index in the collection (1‑based)
                    string newImagePath = kvp.Value;   // Path to the new image file

                    // Verify the index is within the collection bounds
                    if (index < 1 || index > images.Count)
                    {
                        Console.Error.WriteLine($"Image index {index} out of range (1..{images.Count}).");
                        continue;
                    }

                    // Open the replacement image stream and invoke XImageCollection.Replace
                    using (FileStream fs = File.OpenRead(newImagePath))
                    {
                        images.Replace(index, fs);
                    }
                }

                // Save the modified PDF (lifecycle rule: save inside using block)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Images replaced and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}