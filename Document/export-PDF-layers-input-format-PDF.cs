using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class ExportPdfLayers
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Directory where extracted layer PDFs will be saved
        const string outputDirectory = "ExtractedLayers";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate pages using 1‑based indexing (rule: page-indexing-one-based)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // If the page has no layers, skip it
                    if (page.Layers == null || page.Layers.Count == 0)
                        continue;

                    // Iterate through each layer on the current page
                    foreach (Layer layer in page.Layers)
                    {
                        // Build a safe file name using page number and layer name
                        string safeLayerName = string.IsNullOrWhiteSpace(layer.Name)
                            ? "UnnamedLayer"
                            : layer.Name.Replace(Path.GetInvalidFileNameChars(), '_');

                        string outputPath = Path.Combine(
                            outputDirectory,
                            $"Page_{pageIndex}_Layer_{safeLayerName}.pdf");

                        // Save the layer as an independent PDF file
                        // (Layer.Save(string) is the correct API)
                        layer.Save(outputPath);

                        Console.WriteLine($"Saved layer '{layer.Name}' from page {pageIndex} to '{outputPath}'.");
                    }
                }
            }

            Console.WriteLine("Layer extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during layer extraction: {ex.Message}");
        }
    }
}

// Extension method to replace invalid filename characters
static class StringExtensions
{
    public static string Replace(this string str, char[] chars, char replacement)
    {
        foreach (char c in chars)
            str = str.Replace(c, replacement);
        return str;
    }
}