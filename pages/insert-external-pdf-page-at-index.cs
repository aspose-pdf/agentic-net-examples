using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the target PDF, the external PDF (source), and the result PDF
        const string targetPath = "target.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "merged.pdf";

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
            // Retrieve the page to be inserted from the source document.
            // Aspose.Pdf uses 1‑based indexing, so Pages[1] is the first page.
            Page pageToInsert = sourceDoc.Pages[1];

            // Insert the page into the target document at position 2.
            // The Insert overload that takes a Page preserves the page's
            // size (MediaBox, CropBox, etc.) and rotation.
            targetDoc.Pages.Insert(2, pageToInsert);

            // Save the modified document.
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page inserted and saved to '{outputPath}'.");
    }
}