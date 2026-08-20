using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF, target PDF and the resulting PDF
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";
        const string outputPath = "merged.pdf";

        // 1‑based page numbers (Aspose.Pdf uses 1‑based indexing)
        const int sourcePageNumber = 2;   // page to copy from source PDF
        const int insertPosition   = 3;   // position in target PDF where the page will be inserted

        // Verify that both files exist
        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target file not found.");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document sourceDoc = new Document(sourcePath))
            using (Document targetDoc = new Document(targetPath))
            {
                // Validate requested page numbers against the actual page counts
                if (sourcePageNumber < 1 || sourcePageNumber > sourceDoc.Pages.Count)
                {
                    Console.Error.WriteLine("Invalid source page number.");
                    return;
                }

                if (insertPosition < 1 || insertPosition > targetDoc.Pages.Count + 1)
                {
                    Console.Error.WriteLine("Invalid insert position.");
                    return;
                }

                // Retrieve the page from the source document
                Page pageToInsert = sourceDoc.Pages[sourcePageNumber];

                // Insert the page into the target document at the desired position
                targetDoc.Pages.Insert(insertPosition, pageToInsert);

                // Save the modified target document (PDF format)
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Inserted page {sourcePageNumber} from '{sourcePath}' into '{targetPath}' at position {insertPosition}. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}