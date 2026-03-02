using System;
using System.IO;
using Aspose.Pdf; // Document, Page, Layer

class ExportPdfLayers
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExportedLayers";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Each page may contain zero or more layers
                if (page.Layers == null || page.Layers.Count == 0)
                    continue;

                // Export each layer as an independent PDF file
                foreach (Layer layer in page.Layers)
                {
                    // Build a file name that includes page number and layer id (or name if preferred)
                    string safeLayerId = layer.Id.Replace("/", "_").Replace("\\", "_");
                    string outputFileName = $"Page{pageIndex}_Layer_{safeLayerId}.pdf";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the layer to a new PDF document (lifecycle rule: use Layer.Save(string))
                    layer.Save(outputPath);

                    Console.WriteLine($"Exported layer '{layer.Name}' (Id: {layer.Id}) from page {pageIndex} to '{outputPath}'.");
                }
            }
        }

        Console.WriteLine("Layer export completed.");
    }
}