using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Iterate through all pages and hide (delete) each layer
        foreach (Page page in pdfDocument.Pages)
        {
            // The Layers collection may be null if the page has no layers
            if (page.Layers == null) continue;

            // Collect layers to delete to avoid modifying the collection while iterating
            var layersToDelete = new System.Collections.Generic.List<Layer>();
            foreach (Layer layer in page.Layers)
            {
                // Here we simply hide every layer; adjust the condition if you need to target a specific layer
                layersToDelete.Add(layer);
            }

            // Delete the selected layers – this effectively hides their content
            foreach (Layer layer in layersToDelete)
            {
                layer.Delete();
            }
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"PDF saved with layers hidden: {outputPath}");
    }
}