using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Work with the first page (layers are page‑specific)
        if (pdfDocument.Pages.Count > 0)
        {
            Page page = pdfDocument.Pages[1];

            // Check whether the page contains any layers
            if (page.Layers != null && page.Layers.Count > 0)
            {
                Console.WriteLine($"The page contains {page.Layers.Count} layer(s).");

                // List all layers with basic information
                for (int i = 1; i <= page.Layers.Count; i++)
                {
                    Layer layer = page.Layers[i];
                    Console.WriteLine($"Layer {i}: Id={layer.Id}, Name={layer.Name}, Locked={layer.Locked}");
                }

                // ---- Example 1: Hide a layer by flattening it ----
                // Flattening makes the layer's content permanent and removes the layer from the document.
                // The boolean parameter indicates whether to delete the layer after flattening.
                Layer firstLayer = page.Layers[1];
                firstLayer.Flatten(true);
                Console.WriteLine($"Layer \"{firstLayer.Name}\" has been flattened and removed.");

                // ---- Example 2: Lock a layer (prevents further editing) ----
                if (page.Layers.Count >= 2)
                {
                    Layer secondLayer = page.Layers[2];
                    secondLayer.Lock();
                    Console.WriteLine($"Layer \"{secondLayer.Name}\" has been locked.");
                }
            }
            else
            {
                Console.WriteLine("No layers are present on the first page.");
            }
        }

        // Save the modified PDF document
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Modified PDF saved to: {outputPath}");
    }
}