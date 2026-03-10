using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source and target PDF files
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";

        // Page number to transfer (1‑based indexing as required by Aspose.Pdf)
        const int pageNumber = 2;

        // Verify source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load the source PDF document inside a using block for deterministic disposal
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > sourceDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document contains {sourceDoc.Pages.Count} pages.");
                return;
            }

            // Create a new empty PDF document for the target
            using (Document targetDoc = new Document())
            {
                // Add the specified page from the source to the target document.
                // This copies the page content, preserving all graphics, text, annotations, etc.
                targetDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                // Save the target PDF. Since the format is PDF, no SaveOptions are required.
                targetDoc.Save(targetPath);
            }
        }

        Console.WriteLine($"Successfully transferred page {pageNumber} to '{targetPath}'.");
    }
}