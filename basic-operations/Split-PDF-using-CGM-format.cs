using System;
using System.IO;
using Aspose.Pdf; // Core PDF classes and load options are in this namespace

class Program
{
    static void Main()
    {
        // Input CGM file (will be converted to a PDF document internally)
        const string inputCgmPath = "input.cgm";

        // Directory where the split pages will be saved as individual PDFs
        const string outputDir = "SplitPages";

        // Validate input file
        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputCgmPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the CGM file using CgmLoadOptions (CGM is input‑only)
            using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
            {
                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Create a new PDF document that will contain a single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page from the source document
                        singlePageDoc.Pages.Add(doc.Pages[i]);

                        // Build output file name, e.g., Page_1.pdf, Page_2.pdf, ...
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("Splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}