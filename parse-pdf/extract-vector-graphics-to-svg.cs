using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "VectorGraphics";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a subfolder per page to keep SVG files separate
                    string pageDir = Path.Combine(outputDir, $"Page_{i}");
                    Directory.CreateDirectory(pageDir);

                    // SvgExtractor extracts each vector graphic on the page to its own SVG file
                    SvgExtractor extractor = new SvgExtractor();
                    extractor.Extract(page, pageDir);
                }
            }

            Console.WriteLine($"All vector graphics have been extracted to '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}