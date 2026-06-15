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

        // Load source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Create a new empty PDF document
            using (Document destDoc = new Document())
            {
                // Remove the automatically created blank page
                if (destDoc.Pages.Count > 0)
                    destDoc.Pages.Delete(1);

                // Add pages from source in descending order
                for (int i = srcDoc.Pages.Count; i >= 1; i--)
                {
                    // Insert the page at the end of the destination collection
                    destDoc.Pages.Add(srcDoc.Pages[i]);
                }

                // Optional: reorganize the page tree for better performance
                destDoc.PageNodesToBalancedTree();

                // Save the reordered PDF
                destDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Pages reordered descending and saved to '{outputPath}'.");
    }
}