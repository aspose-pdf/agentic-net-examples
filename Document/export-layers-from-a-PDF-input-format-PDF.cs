using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Layers";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (Document implements IDisposable)
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Skip pages without layers
                    if (page.Layers == null || page.Layers.Count == 0)
                        continue;

                    // Export each layer on the current page
                    for (int layerIndex = 0; layerIndex < page.Layers.Count; layerIndex++)
                    {
                        Layer layer = page.Layers[layerIndex];

                        // Build a safe file name using page number, layer name (if any) and layer id
                        string layerName = string.IsNullOrWhiteSpace(layer.Name)
                            ? $"Layer{layerIndex + 1}"
                            : layer.Name.Replace(Path.GetInvalidFileNameChars(), '_');

                        string outputPath = Path.Combine(
                            outputDir,
                            $"Page{pageIndex}_{layerName}_{layer.Id}.pdf");

                        // Save the layer as an independent PDF document
                        layer.Save(outputPath);
                        Console.WriteLine($"Saved layer '{layer.Name}' from page {pageIndex} to '{outputPath}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Extension method to replace invalid filename characters (helper, not part of Aspose.Pdf)
static class StringExtensions
{
    public static string Replace(this string str, char[] chars, char replacement)
    {
        foreach (char c in chars)
            str = str.Replace(c, replacement);
        return str;
    }
}