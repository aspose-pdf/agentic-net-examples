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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, insert a blank page at the beginning, and save.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Insert a new blank page at position 1 (before the original first page).
            doc.Pages.Insert(1);

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank cover page inserted and saved to '{outputPath}'.");
    }
}