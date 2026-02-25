using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExportedLayers";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                var page = doc.Pages[i];

                // Iterate over all layers on the current page
                foreach (Aspose.Pdf.Layer layer in page.Layers)
                {
                    // Build a safe file name that includes page number and layer name
                    string safeLayerName = string.IsNullOrWhiteSpace(layer.Name) ? "Unnamed" : layer.Name;
                    foreach (char c in Path.GetInvalidFileNameChars())
                        safeLayerName = safeLayerName.Replace(c, '_');

                    string outPath = Path.Combine(outputDir,
                        $"Page_{i}_Layer_{safeLayerName}.pdf");

                    // Save the layer as an independent PDF document
                    layer.Save(outPath);
                    Console.WriteLine($"Exported layer '{layer.Name}' from page {i} to '{outPath}'.");
                }
            }
        }
    }
}