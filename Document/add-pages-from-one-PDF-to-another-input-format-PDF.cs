using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (pages to be added) and the target PDF (receives pages)
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";
        const string outputPath = "merged.pdf";

        // Verify that both input files exist
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load the target document inside a using block for deterministic disposal
            using (Document targetDoc = new Document(targetPath))
            // Load the source document inside a nested using block
            using (Document sourceDoc = new Document(sourcePath))
            {
                // Append all pages from sourceDoc to targetDoc
                targetDoc.Pages.Add(sourceDoc.Pages);

                // Save the merged document as PDF
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Pages from '{sourcePath}' have been added to '{targetPath}'.");
            Console.WriteLine($"Merged PDF saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}