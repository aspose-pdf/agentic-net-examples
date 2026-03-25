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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count >= 3)
            {
                // Delete the third page (1‑based indexing)
                doc.Pages.Delete(3);
            }
            else
            {
                Console.WriteLine("The document has fewer than three pages; no deletion performed.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Third page removed (if present). Result saved to '{outputPath}'.");
    }
}