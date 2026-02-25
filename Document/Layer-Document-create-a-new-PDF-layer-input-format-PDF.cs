using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_layer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page (Aspose.Pdf uses 1‑based page indexing)
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Access the first page
            Page page = doc.Pages[1];

            // Create a new layer with a name and an optional content group identifier
            Layer newLayer = new Layer("MyLayer", "OCG1");

            // Add the layer to the page's layer collection
            page.Layers.Add(newLayer);

            // (Optional) Add content that will appear on this layer.
            // For simplicity, we add a text fragment; Aspose.Pdf places new content in the default layer,
            // but the layer is now part of the page and can be manipulated (e.g., flattened, locked) later.
            // TextFragment tf = new TextFragment("Hello from MyLayer");
            // page.Paragraphs.Add(tf);

            // Save the modified PDF (saving to a .pdf extension writes PDF regardless of SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with new layer saved to '{outputPath}'.");
    }
}