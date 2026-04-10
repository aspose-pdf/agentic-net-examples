using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_doubled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Store the original page count (1‑based indexing)
            int originalCount = doc.Pages.Count;

            // Insert a blank page after each original page.
            // Iterate backwards so that inserted pages do not affect the
            // positions of pages we have yet to process.
            for (int i = originalCount; i >= 1; i--)
            {
                // Insert an empty page at position i+1 (after page i)
                doc.Pages.Insert(i + 1);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with doubled pages: {outputPath}");
    }
}