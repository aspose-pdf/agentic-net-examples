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

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Delete pages 5 through 10 (inclusive) by iterating backwards
            // to avoid index shifting after each deletion.
            for (int pageNum = 10; pageNum >= 5; pageNum--)
            {
                doc.Pages.Delete(pageNum);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 5‑10 removed. Saved to '{outputPath}'.");
    }
}
