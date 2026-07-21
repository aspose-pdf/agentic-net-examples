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
        const int sourcePageNumber = 1;           // Page number in source (1‑based)
        const int insertPosition = 2;             // Position in target where page will be inserted (1‑based)

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
            // Load the source document (the page to be inserted)
            using (Document sourceDoc = new Document(sourcePath))
            {
                // Load the target document (where the page will be inserted)
                using (Document targetDoc = new Document(targetPath))
                {
                    // Retrieve the page from the source document (1‑based indexing)
                    Page pageToInsert = sourceDoc.Pages[sourcePageNumber];

                    // Insert the page into the target document at the specified position
                    // Insert overload: Insert(int pageNumber, Page entity)
                    targetDoc.Pages.Insert(insertPosition, pageToInsert);

                    // Save the modified target document
                    targetDoc.Save(outputPath);
                }
            }

            Console.WriteLine($"Inserted page {sourcePageNumber} from '{sourcePath}' into '{targetPath}' at position {insertPosition}.");
            Console.WriteLine($"Result saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}