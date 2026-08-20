using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered_descending.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Create a new empty PDF to hold pages in the desired order
            using (Document destDoc = new Document())
            {
                // Pages are 1‑based; iterate from the last page down to the first
                for (int i = srcDoc.Pages.Count; i >= 1; i--)
                {
                    // Add a copy of the page to the destination document
                    destDoc.Pages.Add(srcDoc.Pages[i]);
                }

                // Optional: balance the page tree for better performance
                destDoc.PageNodesToBalancedTree();

                // Save the reordered PDF
                destDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}