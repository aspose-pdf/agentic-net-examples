using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "large_input.pdf";          // Path to the source PDF
        const string outputRoot = "SplitPages";             // Root folder for all page folders

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the source PDF (lifecycle: load)
            using (Document sourceDoc = new Document(inputPdf))
            {
                int pageCount = sourceDoc.Pages.Count; // 1‑based page count

                // Iterate over each page (page indexing is 1‑based)
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new empty PDF document (lifecycle: create)
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page from the source document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Prepare a folder for this page
                        string pageFolder = Path.Combine(outputRoot, $"Page_{i}");
                        Directory.CreateDirectory(pageFolder);

                        // Save the single‑page PDF into its folder (lifecycle: save)
                        string outputPath = Path.Combine(pageFolder, $"Page_{i}.pdf");
                        singlePageDoc.Save(outputPath);

                        Console.WriteLine($"Saved page {i} to '{outputPath}'");
                    }
                }
            }

            Console.WriteLine("Batch splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}