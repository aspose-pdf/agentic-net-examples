using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";   // PDF to which a page will be appended
        const string sourcePath = "source.pdf";   // PDF containing the page to copy
        const string outputPath = "merged.pdf";   // Resulting PDF

        // Verify that both input files exist
        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document target = new Document(targetPath))
        using (Document source = new Document(sourcePath))
        {
            // Ensure the source PDF has at least one page
            if (source.Pages.Count > 0)
            {
                // Aspose.Pdf uses 1‑based indexing for pages
                // Append the first page of the source PDF to the end of the target PDF
                target.Pages.Add(source.Pages[1]);
            }

            // Save the modified document
            target.Save(outputPath);
        }

        Console.WriteLine($"Page appended successfully. Output saved to '{outputPath}'.");
    }
}