using System;
using System.IO;
using Aspose.Pdf;

class ExportPdfLayers
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where extracted layers will be saved
        const string outputDir = "ExtractedLayers";

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Each page may contain zero or more layers
                foreach (Layer layer in page.Layers)
                {
                    // Build a unique file name for the layer
                    // Layer.Name may contain characters unsuitable for file names; replace them if necessary
                    string safeLayerName = string.Join("_", layer.Name.Split(Path.GetInvalidFileNameChars()));
                    string outputPath = Path.Combine(
                        outputDir,
                        $"page_{page.Number}_layer_{safeLayerName}.pdf");

                    // Save the layer as an independent PDF document
                    layer.Save(outputPath);

                    Console.WriteLine($"Saved layer '{layer.Name}' of page {page.Number} to '{outputPath}'.");
                }
            }
        }

        Console.WriteLine("Layer extraction completed.");
    }
}