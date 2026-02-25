using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string targetPath = "target.pdf";   // PDF that will receive pages
        const string sourcePath = "source.pdf";   // PDF whose pages will be added
        const string outputPath = "merged.pdf";   // Resulting merged PDF

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

        // Wrap both Document objects in using blocks for deterministic disposal
        using (Document target = new Document(targetPath))
        using (Document source = new Document(sourcePath))
        {
            // Append all pages from the source document to the target document
            target.Pages.Add(source.Pages);

            // Save the merged document; Document.Save(string) always writes PDF
            target.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}