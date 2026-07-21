using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sorted_desc.pdf";

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
                // Add pages from the source document in descending order
                // Page numbers are 1‑based, so start from Count and go down to 1
                for (int i = srcDoc.Pages.Count; i >= 1; i--)
                {
                    // Add the page object to the destination document
                    destDoc.Pages.Add(srcDoc.Pages[i]);
                }

                // Optional: reorganize the internal page tree for better performance
                destDoc.PageNodesToBalancedTree();

                // Save the reordered PDF
                destDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Pages reordered descending and saved to '{outputPath}'.");
    }
}