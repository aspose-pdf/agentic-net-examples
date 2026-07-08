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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document sourceDoc = new Document(inputPath))
        {
            // Create a new empty PDF document to hold pages in descending order
            using (Document targetDoc = new Document())
            {
                // Pages are 1‑based; iterate from last to first
                for (int i = sourceDoc.Pages.Count; i >= 1; i--)
                {
                    // Add a copy of each page to the target document
                    targetDoc.Pages.Add(sourceDoc.Pages[i]);
                }

                // Optional: balance the page tree for better performance
                targetDoc.PageNodesToBalancedTree();

                // Save the reordered PDF
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}