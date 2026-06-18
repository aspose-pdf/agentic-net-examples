using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";   // PDF to receive the new page
        const string sourcePath = "source.pdf";   // PDF providing the page to insert
        const string outputPath = "merged.pdf";   // Resulting PDF

        // Verify that both input files exist
        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input files were not found.");
            return;
        }

        // Load the target and source documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPath))
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Aspose.Pdf uses 1‑based indexing; get the first page from the source PDF
            Page pageToInsert = sourceDoc.Pages[1];

            // Insert the page at position 2 in the target document.
            // The Insert overload copies the page, preserving its original size and rotation.
            targetDoc.Pages.Insert(2, pageToInsert);

            // Save the modified document
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page inserted successfully. Output saved to '{outputPath}'.");
    }
}