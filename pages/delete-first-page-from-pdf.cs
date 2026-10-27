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
            // Delete the first page (page number 1) and shift remaining pages forward.
            doc.Pages.Delete(1);

            // Save the modified document back to PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page removed. Saved to '{outputPath}'.");
    }
}