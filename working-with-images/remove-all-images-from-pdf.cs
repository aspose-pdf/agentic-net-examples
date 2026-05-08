using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "images_removed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Delete all images from the current page
                // XImageCollection provides a Delete() method that removes every image
                page.Resources.Images.Delete();
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All images removed. Saved to '{outputPath}'.");
    }
}