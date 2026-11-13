using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Calculate the insertion point: after half of the existing pages.
            int middleIndex = (doc.Pages.Count / 2) + 1;

            // Insert ten empty pages at the calculated position.
            for (int i = 0; i < 10; i++)
            {
                // The overload without a Page argument creates a blank page.
                doc.Pages.Insert(middleIndex);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted 10 empty pages at the midpoint. Saved to '{outputPath}'.");
    }
}
