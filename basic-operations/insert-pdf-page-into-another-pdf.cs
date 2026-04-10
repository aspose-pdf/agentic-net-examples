using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";   // PDF that will receive the new page
        const string sourcePath = "source.pdf";   // PDF containing the page to insert
        const string outputPath = "merged.pdf";   // Resulting PDF
        const int insertPosition = 2;             // 1‑based position where the page will be inserted

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

        // Load both documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPath))
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Retrieve the page to be inserted (pages are 1‑based)
            Page pageToInsert = sourceDoc.Pages[1];

            // Insert the page into the target document at the specified position
            // Insert overload: Insert(int pageNumber, Page entity)
            targetDoc.Pages.Insert(insertPosition, pageToInsert);

            // Save the modified document (PDF format)
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page inserted and saved to '{outputPath}'.");
    }
}