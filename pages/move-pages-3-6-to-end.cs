using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 6 pages (pages are 1‑based)
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document contains fewer than 6 pages.");
                return;
            }

            // Collect pages 3 through 6 (skip first two, then take four)
            var pagesToMove = doc.Pages
                                 .Skip(2)   // pages 1 and 2 are skipped
                                 .Take(4)   // pages 3,4,5,6 are taken
                                 .ToList(); // materialize as a List<Page>

            // Insert the collected pages at the end of the document.
            // Insert expects a 1‑based index; Count+1 places them after the last page.
            doc.Pages.Insert(doc.Pages.Count + 1, pagesToMove);

            // Save the reordered PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 moved to the end. Output saved to '{outputPath}'.");
    }
}