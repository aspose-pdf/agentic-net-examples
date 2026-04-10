using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Total pages before insertion (1‑based indexing).
            int pageCount = doc.Pages.Count;

            // Calculate the position where the new pages will be inserted.
            // Adding 1 places the insertion point at the start of the second half.
            int middle = (pageCount / 2) + 1;

            // Insert ten empty pages at the midpoint.
            for (int i = 0; i < 10; i++)
            {
                doc.Pages.Insert(middle);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted 10 empty pages at the midpoint. Output saved to '{outputPath}'.");
    }
}