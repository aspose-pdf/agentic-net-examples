using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load source document and create a target document inside using blocks
        using (Document src = new Document(inputPath))
        using (Document dst = new Document())
        {
            // Append odd‑numbered pages first (1‑based indexing)
            for (int i = 1; i <= src.Pages.Count; i += 2)
            {
                dst.Pages.Add(src.Pages[i]);
            }

            // Then append even‑numbered pages preserving their original order
            for (int i = 2; i <= src.Pages.Count; i += 2)
            {
                dst.Pages.Add(src.Pages[i]);
            }

            // Save the reordered document
            dst.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}