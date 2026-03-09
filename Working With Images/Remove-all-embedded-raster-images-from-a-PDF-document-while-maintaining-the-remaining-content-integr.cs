using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_images.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all images on the page
                // XImageCollection is enumerable; no dictionary semantics
                foreach (XImage img in page.Resources.Images)
                {
                    // Delete each image by its name (if name is available)
                    // XImage has a Name property that identifies the resource key
                    // The Delete method removes the image from the collection
                    page.Resources.Images.Delete(img.Name);
                }
            }

            // Optimize resources to clean up any dangling references after deletion
            doc.OptimizeResources();

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Raster images removed. Saved to '{outputPath}'.");
    }
}