using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hidden_layer.pdf";
        const string layerNameToHide = "MyLayer";   // name of the layer to hide

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

                // The Layers collection may be null; guard against it
                if (page.Layers == null) continue;

                // Find the layer with the specified name
                foreach (Layer layer in page.Layers)
                {
                    if (string.Equals(layer.Name, layerNameToHide, StringComparison.OrdinalIgnoreCase))
                    {
                        // Deleting the layer removes it from the page, effectively hiding its content
                        layer.Delete();
                        // If you prefer to keep the layer but make it invisible, you could
                        // call layer.Lock() and then flatten, but Delete() is the simplest way.
                        break; // layer names are unique per page; exit after handling
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Layer \"{layerNameToHide}\" hidden (deleted) and saved to '{outputPath}'.");
    }
}