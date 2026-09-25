using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "output.pdf";
        const int insertPosition = 2; // Insert at position 2 (after the first page)

        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input files were not found.");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPath))
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Ensure the source PDF has at least one page to insert
            if (sourceDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Source PDF contains no pages.");
                return;
            }

            // Retrieve the page to insert (1‑based indexing)
            Page pageToInsert = sourceDoc.Pages[1];

            // Insert the page into the target document at the desired position.
            // Pages.Insert shifts existing pages forward; index is 1‑based.
            targetDoc.Pages.Insert(insertPosition, pageToInsert);

            // Save the modified document as PDF
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page inserted successfully. Saved to '{outputPath}'.");
    }
}