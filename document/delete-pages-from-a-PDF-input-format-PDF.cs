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
            // Example: delete pages 2, 4, and 5 (page numbers are 1‑based)
            int[] pagesToDelete = { 2, 4, 5 };
            doc.Pages.Delete(pagesToDelete);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages deleted. Result saved to '{outputPath}'.");
    }
}