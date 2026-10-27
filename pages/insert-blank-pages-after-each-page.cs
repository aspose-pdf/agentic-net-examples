using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "doubled_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Store the original page count (Aspose.Pdf uses 1‑based indexing)
            int originalCount = doc.Pages.Count;

            // Iterate backwards so that inserting pages does not affect the
            // indices of pages we have yet to process
            for (int i = originalCount; i >= 1; i--)
            {
                // Insert a new blank page after the current page (i + 1)
                // The overload without a Page argument creates an empty page.
                doc.Pages.Insert(i + 1);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank pages inserted. Output saved to '{outputPath}'.");
    }
}
