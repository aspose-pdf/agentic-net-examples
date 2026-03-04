using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load source PDF (using ensures proper disposal)
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate pages using 1‑based indexing
                for (int pageNumber = 1; pageNumber <= srcDoc.Pages.Count; pageNumber++)
                {
                    // Create a new empty PDF for the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page from the source document
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageNumber]);

                        // Build output file path
                        string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {pageNumber} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF splitting completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}