using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";   // PDF to receive the new page
        const string sourcePath = "source.pdf";   // PDF containing the page to insert
        const string outputPath = "merged.pdf";   // Resulting PDF

        // Ensure input files exist
        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Wrap both Document objects in using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPath))
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Get the first page from the source PDF (adjust if a different page is needed)
            Page pageToInsert = sourceDoc.Pages[1];

            // Insert the page at index 2 in the target PDF.
            // The Insert method also uses 1‑based indexing.
            // The page's original size and rotation are preserved automatically.
            targetDoc.Pages.Insert(2, pageToInsert);

            // Save the modified document
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page inserted and saved to '{outputPath}'.");
    }
}