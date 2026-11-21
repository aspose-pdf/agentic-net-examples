using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the sequence of page counts to add.
        // For example, {2,3,1} will add 2 pages, then 3 pages, then 1 page.
        List<int> pageCounts = new List<int> { 2, 3, 1 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF. Document is wrapped in a using block per the disposal rule.
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each desired count and add that many empty pages.
            foreach (int count in pageCounts)
            {
                // Guard against non‑positive values.
                if (count <= 0) continue;

                for (int i = 0; i < count; i++)
                {
                    // Add a blank page to the document.
                    doc.Pages.Add();
                }
            }

            // Save the updated PDF. No SaveOptions needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages added successfully. Output saved to '{outputPath}'.");
    }
}