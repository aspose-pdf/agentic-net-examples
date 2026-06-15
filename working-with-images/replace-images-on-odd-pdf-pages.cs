using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesOnOddPages
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string placeholderImagePath = "placeholder.jpg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(placeholderImagePath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Process only odd‑numbered pages
                if (pageNum % 2 == 0) continue;

                Page page = doc.Pages[pageNum];
                var images = page.Resources.Images; // XImageCollection

                // Replace each image on the page with the placeholder
                // XImageCollection uses 1‑based indexing for Replace
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Open placeholder image stream (read‑only)
                    using (FileStream placeholderStream = File.OpenRead(placeholderImagePath))
                    {
                        // Replace the image resource while keeping the original placement rectangle
                        images.Replace(imgIndex, placeholderStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images on odd pages replaced. Output saved to '{outputPdfPath}'.");
    }
}