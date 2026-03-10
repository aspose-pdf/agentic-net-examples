using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source and target PDF files
        const string targetPath = "target.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "merged.pdf";

        // Verify that both input files exist
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

        try
        {
            // Load the target PDF (the document that will receive pages)
            using (Document targetDoc = new Document(targetPath))
            // Load the source PDF (the document whose pages will be added)
            using (Document sourceDoc = new Document(sourcePath))
            {
                // Add all pages from the source document to the target document
                // PageCollection.Add accepts another PageCollection
                targetDoc.Pages.Add(sourceDoc.Pages);

                // Save the merged result
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