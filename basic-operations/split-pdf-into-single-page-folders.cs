using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "large_document.pdf";   // source PDF
        const string outputRoot = "SplitPages";          // root folder for output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the source PDF (lifecycle: load)
        using (Document srcDoc = new Document(inputPdf))
        {
            int pageCount = srcDoc.Pages.Count; // 1‑based page count

            // Iterate over each page (1‑based indexing)
            for (int i = 1; i <= pageCount; i++)
            {
                // Create a new PDF document for the single page (lifecycle: create)
                using (Document singlePageDoc = new Document())
                {
                    // Add the current page to the new document
                    singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                    // Prepare a folder for this page
                    string pageFolder = Path.Combine(outputRoot, $"Page_{i}");
                    Directory.CreateDirectory(pageFolder);

                    // Save the single‑page PDF into its folder (lifecycle: save)
                    string outPath = Path.Combine(pageFolder, $"page_{i}.pdf");
                    singlePageDoc.Save(outPath);

                    Console.WriteLine($"Saved page {i} to '{outPath}'");
                }
            }
        }

        Console.WriteLine("Batch split completed.");
    }
}