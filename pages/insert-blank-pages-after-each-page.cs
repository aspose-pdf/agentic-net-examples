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
            // Store the original page count (1‑based indexing)
            int originalCount = doc.Pages.Count;

            // Iterate backwards to avoid index shifting when inserting pages
            for (int i = originalCount; i >= 1; i--)
            {
                // Insert a blank page after the current page (position i+1)
                doc.Pages.Insert(i + 1);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank pages inserted. Output saved to '{outputPath}'.");
    }
}