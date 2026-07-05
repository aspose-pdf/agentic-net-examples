using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string targetPath = "target.pdf";   // PDF to receive the new page
        const string sourcePath = "source.pdf";   // PDF containing the page to insert
        const string outputPath = "merged.pdf";   // Resulting PDF

        // Ensure input files exist
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPath))
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Get the page to insert from the source document (first page in this example)
            Page pageToInsert = sourceDoc.Pages[1];

            // Insert the page at position 2 in the target document.
            // This preserves the page's original size, media box, crop box, and rotation.
            targetDoc.Pages.Insert(2, pageToInsert);

            // Save the modified document
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page inserted successfully. Output saved to '{outputPath}'.");
    }
}