using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to be split.
        const string inputPdf = "input.pdf";

        // Directory where individual page PDFs will be saved.
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF document.
            using (Document src = new Document(inputPdf))
            {
                // Iterate over all pages (1‑based indexing).
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty PDF document for the single page.
                    using (Document single = new Document())
                    {
                        // Add the current page to the new document.
                        single.Pages.Add(src.Pages[i]);

                        // Build the output file path.
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF.
                        single.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }

        // NOTE: Aspose.Pdf supports CGM as an input format only.
        // There is no CgmSaveOptions class, so PDF cannot be saved as CGM.
        // This example splits a PDF into separate PDF pages.
    }
}