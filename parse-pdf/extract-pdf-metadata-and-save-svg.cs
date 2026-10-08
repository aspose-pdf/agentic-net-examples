using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "VectorPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve basic metadata
            string author = doc.Info.Author;
            string title  = doc.Info.Title;

            Console.WriteLine($"Author: {author}");
            Console.WriteLine($"Title : {title}");

            // Extract each page as an SVG file (vector representation)
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                string svgPath = Path.Combine(outputDir, $"Page_{i}.svg");
                SvgSaveOptions svgOptions = new SvgSaveOptions(); // all SaveOptions are in Aspose.Pdf namespace

                // Create a temporary single‑page document to save only the current page
                using (Document singlePageDoc = new Document())
                {
                    singlePageDoc.Pages.Add(doc.Pages[i]);
                    singlePageDoc.Save(svgPath, svgOptions);
                }

                Console.WriteLine($"Saved vector page {i} to '{svgPath}'.");
            }
        }
    }
}