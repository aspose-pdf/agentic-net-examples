using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string layerExportPath = "layer1.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                Console.WriteLine($"Page {i} has {page.Layers?.Count ?? 0} layer(s).");

                // Work with the first layer if any exist
                if (page.Layers != null && page.Layers.Count > 0)
                {
                    Layer firstLayer = page.Layers[0];
                    Console.WriteLine($"  Layer Id: {firstLayer.Id}, Name: {firstLayer.Name}, Locked: {firstLayer.Locked}");

                    // Flatten the layer (false = keep optional content group markers for speed)
                    firstLayer.Flatten(false);
                    Console.WriteLine("  Layer flattened.");

                    // Save the layer as a separate PDF file
                    firstLayer.Save(layerExportPath);
                    Console.WriteLine($"  Layer saved to '{layerExportPath}'.");

                    // Delete the layer from the page
                    firstLayer.Delete();
                    Console.WriteLine("  Layer deleted.");
                }

                // If more than one layer remains, merge them into a single layer
                if (page.Layers != null && page.Layers.Count > 1)
                {
                    page.MergeLayers("MergedLayer");
                    Console.WriteLine("  Remaining layers merged into 'MergedLayer'.");
                }
            }

            // Save the modified document (lifecycle: save)
            doc.Save(outputPath);
            Console.WriteLine($"Modified document saved to '{outputPath}'.");
        }
    }
}