using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to be split
        const string inputPdf = "input.pdf";
        // Directory where individual pages will be saved
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (using statement ensures proper disposal)
            using (Document srcDoc = new Document(inputPdf))
            {
                // Pages collection is 1‑based in Aspose.Pdf
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new empty document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page to the new document
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build the output file name
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save as PDF (no SaveOptions needed for PDF output)
                        singlePageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF split completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }

        // NOTE:
        // Aspose.Pdf supports CGM as an INPUT format only.
        // There is no CgmSaveOptions class, so a PDF cannot be saved or split as CGM.
        // The code above demonstrates the correct way to split a PDF into separate PDF pages.
    }
}