using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string targetPath = "target.pdf";   // PDF to which a page will be appended
        const string sourcePath = "source.pdf";   // PDF containing the page to append
        const string outputPath = "appended.pdf"; // Resulting PDF

        // Verify that input files exist
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

        // Use nested using blocks for deterministic disposal (rule: document-disposal-with-using)
        using (Document targetDoc = new Document(targetPath))
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Aspose.Pdf uses 1‑based page indexing (rule: page-indexing-one-based)
            // Append the first page of the source PDF to the end of the target PDF
            targetDoc.Pages.Add(sourceDoc.Pages[1]);

            // Save the combined document
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page appended successfully. Output saved to '{outputPath}'.");
    }
}