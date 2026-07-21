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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare an array with page numbers 5 through 10 (1‑based indexing)
            int[] pagesToDelete = new int[6];
            for (int i = 0; i < pagesToDelete.Length; i++)
                pagesToDelete[i] = 5 + i; // pages 5,6,7,8,9,10

            // Delete the specified range in a single call
            doc.Pages.Delete(pagesToDelete);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 5‑10 removed. Saved to '{outputPath}'.");
    }
}