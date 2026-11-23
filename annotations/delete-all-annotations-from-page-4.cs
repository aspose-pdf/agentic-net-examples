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
            // Aspose.Pdf uses 1‑based page indexing
            if (doc.Pages.Count >= 4)
            {
                Page page = doc.Pages[4];

                // Delete all annotations on page 4
                page.Annotations.Delete();

                // Save the modified PDF (PDF format, no SaveOptions needed)
                doc.Save(outputPath);
                Console.WriteLine($"All annotations removed from page 4. Saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
            }
        }
    }
}