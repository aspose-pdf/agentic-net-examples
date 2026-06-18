using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (the page will be taken from here),
        // the target PDF (the page will be inserted into here),
        // and the final output PDF.
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";
        const string outputPath = "merged.pdf";

        // Page number to move – Aspose.Pdf uses 1‑based indexing.
        const int pageToMove = 2;

        // Verify that both input files exist.
        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target file not found.");
            return;
        }

        // Use using blocks for deterministic disposal (document‑disposal‑with‑using rule).
        using (Document sourceDoc = new Document(sourcePath))
        using (Document targetDoc = new Document(targetPath))
        {
            // Ensure the requested page exists in the source document.
            if (pageToMove < 1 || pageToMove > sourceDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Retrieve the page object from the source document.
            Page page = sourceDoc.Pages[pageToMove];

            // Insert the page into the target document.
            // Insert position is 1‑based; adding at the end means Count + 1.
            int insertPos = targetDoc.Pages.Count + 1;
            targetDoc.Pages.Insert(insertPos, page); // preserves rotation and size.

            // Remove the page from the source document to complete the "move".
            sourceDoc.Pages.Delete(pageToMove);

            // Save the modified documents.
            sourceDoc.Save("source_updated.pdf"); // optional: keep the source without the moved page.
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageToMove} moved to '{outputPath}'.");
    }
}