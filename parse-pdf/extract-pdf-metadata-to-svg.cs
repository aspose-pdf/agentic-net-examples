using System;
using System.IO;
using Aspose.Pdf; // Save option classes like SvgSaveOptions are now in the root Aspose.Pdf namespace.

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string metadataOutputPath = "metadata.txt";
        const string svgOutputDir = "VectorPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory for SVG files exists.
        Directory.CreateDirectory(svgOutputDir);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve basic metadata: Title and Author.
            string title = doc.Info.Title ?? "(no title)";
            string author = doc.Info.Author ?? "(no author)";

            // Write metadata to a simple text file.
            File.WriteAllText(metadataOutputPath,
                $"Title: {title}{Environment.NewLine}Author: {author}{Environment.NewLine}");

            Console.WriteLine($"Metadata saved to '{metadataOutputPath}'.");
            Console.WriteLine($"Title : {title}");
            Console.WriteLine($"Author: {author}");

            // Extract vector representation of each page as SVG.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create a temporary document containing only the current page.
                using (Document singlePageDoc = new Document())
                {
                    // Add the page (pages are 1‑based).
                    singlePageDoc.Pages.Add(doc.Pages[i]);

                    // Define SVG save options (no extra settings required for basic export).
                    SvgSaveOptions svgOptions = new SvgSaveOptions();

                    // Build the output file name.
                    string svgPath = Path.Combine(svgOutputDir, $"Page_{i}.svg");

                    // Save the single‑page document as SVG.
                    singlePageDoc.Save(svgPath, svgOptions);

                    Console.WriteLine($"Page {i} saved as SVG to '{svgPath}'.");
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}
