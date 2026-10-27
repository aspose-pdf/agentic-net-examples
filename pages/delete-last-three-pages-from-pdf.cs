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
            int pageCount = doc.Pages.Count;

            // Delete the last three pages, starting from the highest index
            if (pageCount >= 3)
            {
                for (int i = pageCount; i > pageCount - 3; i--)
                {
                    doc.Pages.Delete(i); // PageCollection.Delete uses 1‑based indexing
                }
            }
            else
            {
                Console.WriteLine("Document has fewer than three pages; no pages deleted.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Last three pages removed. Saved to '{outputPath}'.");
    }
}