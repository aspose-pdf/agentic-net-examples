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
                // Retrieve the page to be inserted (first page of source)
                Page pageToInsert = sourceDoc.Pages[1]; // Pages are 1‑based

                // Insert the page into the target document at the specified position
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