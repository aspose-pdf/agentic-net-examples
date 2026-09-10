using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the target PDF, the source PDF (page to insert), and the result PDF
        const string targetPath = "target.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "merged.pdf";

        // Verify that both input files exist
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

        try
        {
            // Load the target document (the one into which we will insert a page)
            using (Document targetDoc = new Document(targetPath))
            // Load the source document (the page we want to insert)
            using (Document sourceDoc = new Document(sourcePath))
            {
                // Choose the page number from the source document to insert.
                // Aspose.Pdf uses 1‑based indexing, so page 1 is the first page.
                Page pageToInsert = sourceDoc.Pages[1];

                // Define the position in the target document where the page will be inserted.
                // For example, insert after the first page (position 2).
                int insertPosition = 2; // 1‑based index

                // Insert the page into the target document at the specified position.
                // The Insert overload copies the page into the target collection.
                targetDoc.Pages.Insert(insertPosition, pageToInsert);

                // Save the modified document.
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