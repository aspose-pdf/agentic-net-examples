using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Ensure the document has at least three pages.
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document has fewer than 3 pages; cannot duplicate page 3.");
                return;
            }

            // Duplicate page 3 and insert the copy immediately after it.
            // Insert position is 4 because we want the copy right after the original page 3.
            // The Insert method creates a copy of the source page.
            doc.Pages.Insert(4, doc.Pages[3]);

            // Save the modified PDF. Document.Save(string) writes PDF regardless of extension.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 3 duplicated and inserted. Output saved to '{outputPath}'.");
    }
}