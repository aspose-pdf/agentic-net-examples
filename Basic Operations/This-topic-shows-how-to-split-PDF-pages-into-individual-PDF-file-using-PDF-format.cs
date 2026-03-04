using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for split pages
        const string outputDir = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF document (lifecycle rule: load)
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate pages using 1‑based indexing (rule: page-indexing-one-based)
                for (int pageNumber = 1; pageNumber <= srcDoc.Pages.Count; pageNumber++)
                {
                    // Create a new empty PDF document for the single page (lifecycle rule: create)
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page to the new document
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageNumber]);

                        // Build output file name
                        string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.pdf");

                        // Save the single‑page PDF (lifecycle rule: save)
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {pageNumber} to '{outPath}'");
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