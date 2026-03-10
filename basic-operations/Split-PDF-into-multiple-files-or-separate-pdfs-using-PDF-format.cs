using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF to split
        const string inputPdfPath = "input.pdf";

        // Directory where split pages will be saved
        const string outputDirectory = "SplitPages";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found -> {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the source PDF (using block ensures proper disposal)
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int pageIndex = 1; pageIndex <= sourceDoc.Pages.Count; pageIndex++)
                {
                    // Create a new empty PDF document for the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the specific page from the source document
                        // (Pages.Add copies the page into the new document)
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageIndex]);

                        // Build output file name (e.g., Page_1.pdf, Page_2.pdf, ...)
                        string outputPath = Path.Combine(outputDirectory, $"Page_{pageIndex}.pdf");

                        // Save the single‑page PDF (PDF format by default)
                        singlePageDoc.Save(outputPath);

                        Console.WriteLine($"Saved page {pageIndex} to '{outputPath}'");
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