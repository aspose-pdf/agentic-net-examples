using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source PDF (document-disposal-with-using rule)
        using (Document srcDoc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                // Create a new empty PDF for the single page
                using (Document singlePageDoc = new Document())
                {
                    // Add the current page to the new document
                    singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                    // Construct the output file path
                    string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                    // Save the single‑page PDF (PDF format)
                    singlePageDoc.Save(outPath);

                    Console.WriteLine($"Saved page {i} → {outPath}");
                }
            }
        }

        Console.WriteLine("Splitting complete.");
    }
}