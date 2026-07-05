using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";   // PDF containing the page to copy
        const string targetPath = "target.pdf";   // PDF to which the page will be appended
        const string outputPath = "merged.pdf";   // Resulting PDF

        // Verify that both input files exist
        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // Use nested using blocks for deterministic disposal (document-disposal-with-using rule)
        using (Document target = new Document(targetPath))
        using (Document source = new Document(sourcePath))
        {
            // Append the first page of the source document to the end of the target document.
            // PageCollection uses 1‑based indexing (page-indexing-one-based rule).
            target.Pages.Add(source.Pages[1]);

            // Save the merged document (save-to-non-pdf-always-use-save-options not needed for PDF)
            target.Save(outputPath);
        }

        Console.WriteLine($"Page appended and saved to '{outputPath}'.");
    }
}