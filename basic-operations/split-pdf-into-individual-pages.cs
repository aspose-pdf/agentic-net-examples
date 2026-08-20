using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large_document.pdf";
        const string outputRoot   = "SplitPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the source PDF (lifecycle: load)
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                int pageCount = sourceDoc.Pages.Count; // 1‑based page count

                // Iterate over each page (1‑based indexing)
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a folder for the current page
                    string pageFolder = Path.Combine(outputRoot, $"Page_{i}");
                    Directory.CreateDirectory(pageFolder);

                    // Create a new PDF containing only the current page (lifecycle: create)
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page from the source document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Save the single‑page PDF into its folder
                        string outputPath = Path.Combine(pageFolder, $"Page_{i}.pdf");
                        singlePageDoc.Save(outputPath); // lifecycle: save
                        Console.WriteLine($"Saved page {i} to '{outputPath}'");
                    }
                }
            }

            Console.WriteLine("Batch split completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}