using System;
using System.IO;
using Aspose.Pdf;               // Core API for PDF manipulation

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
            // Page numbers are 1‑based; delete the third page if it exists
            if (doc.Pages.Count >= 3)
            {
                doc.Pages.Delete(3);   // Delete specified page (index = 3)
            }
            else
            {
                Console.WriteLine("The document has fewer than 3 pages; no deletion performed.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Third page removed (if present). Saved to '{outputPath}'.");
    }
}