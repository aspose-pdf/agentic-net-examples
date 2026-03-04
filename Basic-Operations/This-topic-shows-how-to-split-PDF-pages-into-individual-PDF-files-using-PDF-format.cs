using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Directory where individual pages will be saved
        const string outputDir = "SplitPages";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (document-disposal-with-using rule)
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
                for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
                {
                    // Create a new empty PDF for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page from the source document
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageNum]);

                        // Build the output file name
                        string outPath = Path.Combine(outputDir, $"Page_{pageNum}.pdf");

                        // Save the single‑page PDF (PDF format, no SaveOptions needed)
                        singlePageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {pageNum} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF split completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}