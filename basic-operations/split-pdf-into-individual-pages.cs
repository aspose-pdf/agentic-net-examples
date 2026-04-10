using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large_document.pdf";          // source PDF
        const string outputRoot   = "SplitPages";                 // root folder for all pages

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create (or clean) the root output directory
        if (Directory.Exists(outputRoot))
            Directory.Delete(outputRoot, recursive: true);
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the source PDF – using ensures deterministic disposal
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                int pageCount = sourceDoc.Pages.Count; // 1‑based count

                // Iterate over each page (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a sub‑folder for the current page
                    string pageFolder = Path.Combine(outputRoot, $"Page_{i}");
                    Directory.CreateDirectory(pageFolder);

                    // Create a new PDF containing only the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page from the source document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Save the single‑page PDF inside its folder
                        string outputPdfPath = Path.Combine(pageFolder, $"Page_{i}.pdf");
                        singlePageDoc.Save(outputPdfPath);
                    }

                    Console.WriteLine($"Saved page {i} to '{pageFolder}'.");
                }
            }

            Console.WriteLine("Batch split completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}