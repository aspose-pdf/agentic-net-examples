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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Insert a new blank page at position 3 (before the original page 3).
            doc.Pages.Insert(3); // overload creates a blank page automatically

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page inserted at index 3 and saved to '{outputPath}'.");
    }
}
