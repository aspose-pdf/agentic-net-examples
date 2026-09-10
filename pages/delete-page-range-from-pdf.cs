using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

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
            // Define the page numbers to delete (5 through 10, inclusive)
            int[] pagesToDelete = { 5, 6, 7, 8, 9, 10 };

            // Delete the specified pages in a single operation
            doc.Pages.Delete(pagesToDelete);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 5‑10 removed. Result saved to '{outputPath}'.");
    }
}