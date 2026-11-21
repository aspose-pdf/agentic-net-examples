using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "result.pdf";
        const int insertPosition = 3;      // 1‑based position in the target where the page will be inserted
        const int sourcePageNumber = 1;    // 1‑based page number to take from the source PDF

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

        // Both documents must stay alive until Save() completes.
        using (Document target = new Document(targetPath))
        using (Document source = new Document(sourcePath))
        {
            // Validate page numbers.
            if (sourcePageNumber < 1 || sourcePageNumber > source.Pages.Count)
            {
                Console.Error.WriteLine("Invalid source page number.");
                return;
            }
            if (insertPosition < 1 || insertPosition > target.Pages.Count + 1)
            {
                Console.Error.WriteLine("Invalid insert position.");
                return;
            }

            // Extract the page from the source document.
            Page pageToInsert = source.Pages[sourcePageNumber];

            // Insert the page into the target document at the desired position.
            // PageCollection.Insert uses 1‑based indexing.
            target.Pages.Insert(insertPosition, pageToInsert);

            // Save the modified target document.
            target.Save(outputPath);
        }

        Console.WriteLine($"Page inserted and saved to '{outputPath}'.");
    }
}