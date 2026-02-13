using System;
using System.IO;
using Aspose.Pdf;

class ExportPdfLayers
{
    static void Main(string[] args)
    {
        // Input PDF path (change as needed)
        const string inputPdfPath = "input.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Create an output directory for the exported layers
        string outputDir = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(inputPdfPath)) ?? Directory.GetCurrentDirectory(), "ExportedLayers");
        Directory.CreateDirectory(outputDir);

        // Iterate through each page in the document
        foreach (Page page in pdfDocument.Pages)
        {
            // Each page may contain zero or more layers
            if (page.Layers == null || page.Layers.Count == 0)
                continue;

            // Iterate through the layers of the current page
            foreach (Layer layer in page.Layers)
            {
                // Build a safe file name using page number and layer name (or Id if name is empty)
                string safeLayerName = string.IsNullOrWhiteSpace(layer.Name) ? $"Layer_{layer.Id}" : layer.Name;
                // Remove any characters that are invalid in file names
                foreach (char c in Path.GetInvalidFileNameChars())
                    safeLayerName = safeLayerName.Replace(c, '_');

                string outputPath = Path.Combine(
                    outputDir,
                    $"Page_{page.Number}_Layer_{safeLayerName}.pdf");

                // Save the individual layer to a separate PDF file
                layer.Save(outputPath);

                Console.WriteLine($"Exported layer '{layer.Name}' from page {page.Number} to '{outputPath}'.");
            }
        }

        // Optionally, save the original document (demonstrating the document-save rule)
        string originalCopyPath = Path.Combine(outputDir, "OriginalDocumentCopy.pdf");
        pdfDocument.Save(originalCopyPath); // document-save rule applied

        Console.WriteLine("Layer export completed.");
    }
}