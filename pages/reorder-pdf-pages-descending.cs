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

        // Load the source PDF (load rule)
        using (Document sourceDoc = new Document(inputPath))
        {
            // Create a new empty PDF document (create rule)
            using (Document targetDoc = new Document())
            {
                // Copy pages from source to target in descending order
                for (int i = sourceDoc.Pages.Count; i >= 1; i--)
                {
                    // Add a copy of the page to the target document
                    targetDoc.Pages.Add(sourceDoc.Pages[i]);
                }

                // Optional: reorganize the page tree for better performance
                targetDoc.PageNodesToBalancedTree();

                // Save the reordered PDF (save rule)
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}