using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";   // PDF to receive the new page
        const string sourcePath = "source.pdf";   // PDF containing the page to insert
        const string outputPath = "output.pdf";   // Resulting PDF

        // Verify input files exist
        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document target = new Document(targetPath))
        using (Document source = new Document(sourcePath))
        {
            // Insert the first page from the source PDF at index 2 of the target PDF.
            // Aspose.Pdf copies the page internally, preserving its original size and rotation.
            // Indexing is 1‑based, so position 2 means after the first page.
            target.Pages.Insert(2, source.Pages[1]);

            // Save the modified document.
            target.Save(outputPath);
        }

        Console.WriteLine($"Inserted page saved to '{outputPath}'.");
    }
}
