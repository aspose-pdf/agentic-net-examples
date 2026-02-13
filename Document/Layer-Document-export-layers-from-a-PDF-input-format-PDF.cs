using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF
        string inputPath = "input.pdf";

        // Directory where extracted layers will be stored
        string outputDir = "LayersOutput";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
        for (int pageIdx = 1; pageIdx <= pdfDocument.Pages.Count; pageIdx++)
        {
            Page page = pdfDocument.Pages[pageIdx];

            // Skip pages without layers
            if (page.Layers == null || page.Layers.Count == 0)
                continue;

            // Iterate over each layer on the current page
            for (int layerIdx = 1; layerIdx <= page.Layers.Count; layerIdx++)
            {
                Layer layer = page.Layers[layerIdx];

                // Build a safe file name for the layer PDF
                string safeLayerName = MakeFileSystemSafe(layer.Name);
                string layerFilePath = Path.Combine(
                    outputDir,
                    $"Page_{pageIdx}_Layer_{layerIdx}_{safeLayerName}.pdf");

                // Export the layer to its own PDF file
                layer.Save(layerFilePath);
                Console.WriteLine($"Saved layer '{layer.Name}' (page {pageIdx}) to {layerFilePath}");
            }
        }

        // Example of using the document-save rule to copy the original PDF
        string originalCopyPath = Path.Combine(outputDir, "OriginalCopy.pdf");
        pdfDocument.Save(originalCopyPath);
        Console.WriteLine($"Original document saved to {originalCopyPath}");
    }

    // Replaces characters that are invalid in file names with an underscore
    static string MakeFileSystemSafe(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');

        return string.IsNullOrWhiteSpace(name) ? "UnnamedLayer" : name;
    }
}