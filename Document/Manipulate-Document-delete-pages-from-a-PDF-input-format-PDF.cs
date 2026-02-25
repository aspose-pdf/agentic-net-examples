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
            // Delete specific pages (Aspose.Pdf uses 1‑based indexing)
            // Example: remove page 2 and page 4
            int[] pagesToDelete = { 2, 4 };
            doc.Pages.Delete(pagesToDelete);

            // Save the modified document back to PDF format
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages deleted. Result saved to '{outputPath}'.");
    }
}