using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_images.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Remove all images from the current page.
                // XImageCollection provides a Delete() method that clears the collection.
                page.Resources.Images.Delete();

                // If you prefer to delete by index (simulating RemoveAt), iterate backwards:
                // for (int idx = page.Resources.Images.Count; idx >= 1; idx--)
                //     page.Resources.Images.Delete(idx);
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All images removed. Saved to '{outputPath}'.");
    }
}