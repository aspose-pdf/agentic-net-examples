using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "output.pdf";
        const string placeholderImg = "placeholder.jpg"; // low‑resolution placeholder

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(placeholderImg))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImg}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                var images = page.Resources.Images; // XImageCollection

                // Replace each image on the page with the placeholder
                // XImageCollection uses 1‑based indexes as well
                for (int imgIdx = 1; imgIdx <= images.Count; imgIdx++)
                {
                    using (FileStream placeholderStream = File.OpenRead(placeholderImg))
                    {
                        // Replace the image at the current index
                        images.Replace(imgIdx, placeholderStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All images replaced with placeholder. Saved to '{outputPath}'.");
    }
}