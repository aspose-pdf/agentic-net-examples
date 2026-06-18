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
            int pageCount = doc.Pages.Count;

            // Ensure there are at least three pages to delete
            if (pageCount < 3)
            {
                Console.Error.WriteLine("Document has fewer than three pages; nothing to delete.");
                return;
            }

            // Delete the last three pages by index (1‑based). Delete in descending order.
            for (int i = 0; i < 3; i++)
            {
                // pageCount - i gives the current last page index after previous deletions
                doc.Pages.Delete(pageCount - i);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Last three pages removed. Saved to '{outputPath}'.");
    }
}