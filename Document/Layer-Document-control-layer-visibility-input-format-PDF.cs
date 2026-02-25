using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "layered_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Example 1: Hide a specific layer by deleting it
                // (Assumes the PDF contains at least one layer)
                if (page.Layers != null && page.Layers.Count > 0)
                {
                    // Delete the first layer – it will no longer be visible
                    page.Layers[0].Delete();
                }

                // Example 2: Merge all remaining layers into a single layer
                // This makes the content of all layers visible as one combined layer
                // Provide a new layer name; optional OCG id can be omitted or set
                page.MergeLayers("MergedLayer");

                // Example 3: Flatten a specific layer (if you need to make its content
                // part of the page content stream while optionally cleaning up OCG markers)
                // Here we flatten the merged layer without cleaning up the content stream
                // (cleanupContentStream = false speeds up the operation)
                // Note: After MergeLayers the new layer is the last one in the collection
                if (page.Layers != null && page.Layers.Count > 0)
                {
                    Layer mergedLayer = page.Layers[page.Layers.Count - 1];
                    mergedLayer.Flatten(false);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Layer visibility controlled PDF saved to '{outputPath}'.");
    }
}