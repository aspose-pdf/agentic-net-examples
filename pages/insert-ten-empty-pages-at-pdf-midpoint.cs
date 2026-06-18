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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                int pageCount = doc.Pages.Count;
                // Position after the first half of the pages (midpoint)
                int middle = (pageCount / 2) + 1;

                // Insert ten empty pages at the calculated midpoint
                for (int i = 0; i < 10; i++)
                {
                    doc.Pages.Insert(middle);
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Inserted 10 pages at the midpoint. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}