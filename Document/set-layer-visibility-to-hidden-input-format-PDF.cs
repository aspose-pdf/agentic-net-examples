using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hidden_layers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // The Page class exposes a Layers collection (optional content groups).
                // If a layer should be hidden, we can delete it from the page.
                // Note: Aspose.Pdf does not provide a direct "Visible" property for Layer.
                // Deleting the layer effectively hides its content.
                if (page.Layers != null && page.Layers.Count > 0)
                {
                    // Collect layers to delete (e.g., all layers or based on a name filter)
                    var layersToDelete = new System.Collections.Generic.List<Layer>();
                    foreach (Layer layer in page.Layers)
                    {
                        // Example condition: hide layers whose name starts with "Hidden"
                        if (!string.IsNullOrEmpty(layer.Name) && layer.Name.StartsWith("Hidden", StringComparison.OrdinalIgnoreCase))
                        {
                            layersToDelete.Add(layer);
                        }
                    }

                    // Delete the selected layers
                    foreach (Layer layer in layersToDelete)
                    {
                        layer.Delete(); // Removes the layer from the document
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden layers: {outputPath}");
    }
}