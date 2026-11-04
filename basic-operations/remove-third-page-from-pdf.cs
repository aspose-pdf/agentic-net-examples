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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Delete the third page (page number 3) if it exists.
            if (doc.Pages.Count >= 3)
            {
                doc.Pages.Delete(3);
            }
            else
            {
                Console.WriteLine("The document has fewer than three pages; nothing to delete.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Third page removed (if present). Result saved to '{outputPath}'.");
    }
}