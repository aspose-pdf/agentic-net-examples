using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "appended.pdf";

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

        // Load both PDFs inside using blocks for deterministic disposal.
        using (Document target = new Document(targetPath))
        using (Document source = new Document(sourcePath))
        {
            // Append the first page of the source PDF to the end of the target PDF.
            // Aspose.Pdf uses 1‑based page indexing (page-indexing-one-based rule).
            target.Pages.Add(source.Pages[1]);

            // Save the resulting document.
            target.Save(outputPath);
        }

        Console.WriteLine($"Page appended and saved to '{outputPath}'.");
    }
}