using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_pages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            int pageCount = srcDoc.Pages.Count; // 1‑based page count

            // Iterate through each page
            for (int i = 1; i <= pageCount; i++)
            {
                // Create a new document for the single page
                using (Document singlePageDoc = new Document())
                {
                    // Add the current page to the new document
                    singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                    // Build the output file name
                    string outPath = Path.Combine(outputDir, $"page_{i}.pdf");

                    // Save the single‑page PDF
                    singlePageDoc.Save(outPath);
                    Console.WriteLine($"Saved page {i} → {outPath}");
                }
            }
        }
    }
}