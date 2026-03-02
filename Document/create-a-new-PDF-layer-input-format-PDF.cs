using System;
using System.IO;
using Aspose.Pdf; // Provides Document, Page, Layer

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_layer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            if (doc.Pages.Count == 0)
            {
                doc.Pages.Add();
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new layer with a name and an optional content group ID
            Layer newLayer = new Layer("MyNewLayer", "OCG1");

            // Add the layer to the page's layer collection
            page.Layers.Add(newLayer);

            // (Optional) Add content to the layer here if needed

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new layer: {outputPath}");
    }
}