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
            // Aspose.Pdf uses 1‑based page indexing; page 3 is at index 3
            if (doc.Pages.Count >= 3)
            {
                Page page = doc.Pages[3];

                // Remove all annotations from the page
                page.Annotations.Clear();
            }
            else
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations removed from page 3. Saved to '{outputPath}'.");
    }
}