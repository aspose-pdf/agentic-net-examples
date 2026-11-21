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

        // Load the source PDF
        using (Document source = new Document(inputPath))
        {
            // Create an empty target PDF
            using (Document target = new Document())
            {
                // Append odd‑numbered pages first (1‑based indexing)
                for (int i = 1; i <= source.Pages.Count; i += 2)
                {
                    // Add copies of the pages to the target document
                    target.Pages.Add(source.Pages[i]);
                }

                // Append even‑numbered pages preserving their original order
                for (int i = 2; i <= source.Pages.Count; i += 2)
                {
                    target.Pages.Add(source.Pages[i]);
                }

                // Save the reordered document
                target.Save(outputPath);
            }
        }

        Console.WriteLine($"Pages reordered and saved to '{outputPath}'.");
    }
}