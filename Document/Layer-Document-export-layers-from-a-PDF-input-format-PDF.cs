using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExportedLayers";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Each page may contain one or more layers
                foreach (Layer layer in page.Layers)
                {
                    // Build a unique file name for the exported layer
                    string safeLayerName = string.IsNullOrWhiteSpace(layer.Name) ? "Unnamed" : layer.Name;
                    // Remove any invalid file name characters
                    foreach (char c in Path.GetInvalidFileNameChars())
                        safeLayerName = safeLayerName.Replace(c, '_');

                    string outputPath = Path.Combine(
                        outputDir,
                        $"Page{pageIndex}_Layer_{safeLayerName}.pdf");

                    // Save the layer as a separate PDF document
                    layer.Save(outputPath);
                    Console.WriteLine($"Exported layer '{layer.Name}' from page {pageIndex} to '{outputPath}'.");
                }
            }
        }

        Console.WriteLine("Layer export completed.");
    }
}