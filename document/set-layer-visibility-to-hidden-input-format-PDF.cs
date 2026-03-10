using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hidden_layer.pdf";
        const string layerName  = "MyLayer"; // name of the layer to hide

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Aspose.Pdf provides a Layers collection on each page.
                // Find the layer with the specified name.
                Layer targetLayer = null;
                foreach (Layer layer in page.Layers)
                {
                    if (string.Equals(layer.Name, layerName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetLayer = layer;
                        break;
                    }
                }

                // If the layer exists, delete it – this effectively hides the layer's content.
                if (targetLayer != null)
                {
                    targetLayer.Delete();
                    Console.WriteLine($"Layer \"{layerName}\" deleted from page {i}.");
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with hidden layer: {outputPath}");
    }
}