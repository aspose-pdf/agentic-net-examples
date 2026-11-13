using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete the last three pages, one at a time.
            // PageCollection.Delete uses 1‑based indexing, so we delete the current last page repeatedly.
            for (int i = 0; i < 3; i++)
            {
                if (doc.Pages.Count == 0) break; // safety check
                doc.Pages.Delete(doc.Pages.Count);
            }

            // Save the modified document (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Deleted last three pages. Saved to '{outputPath}'.");
    }
}