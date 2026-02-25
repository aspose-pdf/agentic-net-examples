using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_layers_merged.pdf";
        const string layerFile  = "single_layer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // ------------------------------------------------------------
                // Example 1: List existing layers on the page
                // ------------------------------------------------------------
                Console.WriteLine($"Page {i} has {page.Layers?.Count ?? 0} layer(s).");
                if (page.Layers != null)
                {
                    foreach (Layer existingLayer in page.Layers)
                    {
                        Console.WriteLine($"  Layer Name: {existingLayer.Name}, Id: {existingLayer.Id}");
                    }
                }

                // ------------------------------------------------------------
                // Example 2: Merge all existing layers into a single layer
                // ------------------------------------------------------------
                // The new layer will be named "MergedLayer". Optional OCG Id can be omitted.
                page.MergeLayers("MergedLayer");

                // ------------------------------------------------------------
                // Example 3: Flatten the newly created merged layer
                // ------------------------------------------------------------
                // After merging, the page now contains a single layer with the given name.
                // Retrieve it (it will be the first element in the Layers collection).
                if (page.Layers != null && page.Layers.Count > 0)
                {
                    Layer mergedLayer = page.Layers[0];
                    // Flatten the layer and clean up the optional content group markers.
                    mergedLayer.Flatten(true);
                }

                // ------------------------------------------------------------
                // Example 4: Save a specific layer to its own PDF file (optional)
                // ------------------------------------------------------------
                // Here we demonstrate saving the merged layer of the first page only.
                if (i == 1 && page.Layers != null && page.Layers.Count > 0)
                {
                    Layer mergedLayer = page.Layers[0];
                    mergedLayer.Save(layerFile);
                    Console.WriteLine($"Saved merged layer of page 1 to '{layerFile}'.");
                }

                // ------------------------------------------------------------
                // Example 5: Add a new layer with custom content (optional)
                // ------------------------------------------------------------
                // Create a new layer named "AnnotationLayer" with a custom OCG Id.
                Layer newLayer = new Layer("AnnotationLayer", "OCG_Annotation");
                // Add the layer to the page's layer collection.
                page.Layers.Add(newLayer);

                // Add a simple text annotation to the page; the text will belong to the new layer.
                // Note: TextAnnotation automatically respects the current layer context.
                TextFragment tf = new TextFragment("Sample text in a new layer");
                tf.Position = new Position(100, 700); // Position within the page
                tf.TextState.FontSize = 12;
                page.Paragraphs.Add(tf);
            }

            // Save the modified document. The Save method writes PDF regardless of extension.
            doc.Save(outputPath);
            Console.WriteLine($"Document with merged layers saved to '{outputPath}'.");
        }
    }
}