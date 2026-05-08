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

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Capture the original page count before any modifications
            int originalPageCount = doc.Pages.Count;

            // Insert a blank page after each existing page.
            // Iterate backwards to avoid shifting indices of pages yet to be processed.
            for (int i = originalPageCount; i >= 1; i--)
            {
                // Insert an empty page at position i+1 (after the current page)
                doc.Pages.Insert(i + 1);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank pages inserted. Output saved to '{outputPath}'.");
    }
}