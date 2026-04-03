using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string placeholderPath = "placeholder.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(placeholderPath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderPath}");
            return;
        }

        // Load the entire PDF document
        using (Document doc = new Document(inputPath))
        {
            // Read placeholder image once into memory
            byte[] placeholderBytes = File.ReadAllBytes(placeholderPath);

            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                var images = page.Resources.Images;

                // Replace each image on the page with the placeholder
                int imageCount = images.Count; // 1‑based count
                for (int imgIdx = 1; imgIdx <= imageCount; imgIdx++)
                {
                    // Create a fresh stream for each replacement
                    using (MemoryStream ms = new MemoryStream(placeholderBytes))
                    {
                        images.Replace(imgIdx, ms);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All images replaced with placeholder. Saved to '{outputPath}'.");
    }
}