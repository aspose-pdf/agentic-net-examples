using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";   // PDF containing the page to insert
        const string targetPath = "target.pdf";   // PDF into which the page will be inserted
        const string outputPath = "merged.pdf";   // Resulting PDF
        const int insertPosition = 2;             // 1‑based position in target where the page will be placed

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
            // Load both documents inside using blocks for deterministic disposal
            using (Document sourceDoc = new Document(sourcePath))
            using (Document targetDoc = new Document(targetPath))
            {
                // Validate the insert position (must be between 1 and Pages.Count + 1)
                int maxPosition = targetDoc.Pages.Count + 1;
                if (insertPosition < 1 || insertPosition > maxPosition)
                {
                    Console.Error.WriteLine($"Insert position {insertPosition} is out of range. Valid range: 1‑{maxPosition}.");
                    return;
                }

                // Directly insert the page from the source document.
                // This moves the page from sourceDoc to targetDoc; sourceDoc can be disposed afterwards.
                Page pageToInsert = sourceDoc.Pages[1];
                targetDoc.Pages.Insert(insertPosition, pageToInsert);

                // Save the modified target document
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Page inserted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
