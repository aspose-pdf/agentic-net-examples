using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";   // PDF that will receive the new page
        const string sourcePath = "source.pdf";   // PDF that provides the page to insert
        const string outputPath = "result.pdf";   // Path for the merged document
        const int insertPosition = 3;             // 1‑based index where the page will be placed

        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input files were not found.");
            return;
        }

        // Load both documents inside using blocks (ensures deterministic disposal)
        using (Document target = new Document(targetPath))
        using (Document source = new Document(sourcePath))
        {
            // Validate the insertion index (must be between 1 and Pages.Count+1)
            int position = Math.Max(1, Math.Min(insertPosition, target.Pages.Count + 1));

            // Retrieve the page to insert from the source document (Aspose.Pdf uses 1‑based indexing)
            Page pageToInsert = source.Pages[1];

            // Insert the page into the target document at the desired position
            target.Pages.Insert(position, pageToInsert);

            // Save the updated document
            target.Save(outputPath);
        }

        Console.WriteLine($"Page inserted successfully. Output saved to '{outputPath}'.");
    }
}