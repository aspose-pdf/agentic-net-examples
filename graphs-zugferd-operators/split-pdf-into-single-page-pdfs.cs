using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (lifecycle: load)
            using (Document sourceDoc = new Document(inputPdf))
            {
                // Iterate using 1‑based page indexing (Aspose.Pdf rule)
                for (int pageNumber = 1; pageNumber <= sourceDoc.Pages.Count; pageNumber++)
                {
                    // Create a new PDF document for the single page (lifecycle: create)
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page to the new document. Aspose.Pdf internally copies the page,
                        // so no explicit Clone method is required.
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                        // Build output file name
                        string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.pdf");

                        // Save the single‑page PDF (lifecycle: save)
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {pageNumber} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}
