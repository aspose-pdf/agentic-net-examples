using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_images.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Remove all images from the page.
                // Iterate backwards because the collection is 1‑based and Delete shifts indices.
                for (int imgIndex = page.Resources.Images.Count; imgIndex >= 1; imgIndex--)
                {
                    page.Resources.Images.Delete(imgIndex);
                }
            }

            // Save the modified PDF (output format is PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All images removed. Saved to '{outputPath}'.");
    }
}
