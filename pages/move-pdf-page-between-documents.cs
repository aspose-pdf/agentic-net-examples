using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";   // PDF containing the page to move
        const string targetPath = "target.pdf";   // PDF that will receive the page
        const string outputSourcePath = "source_updated.pdf"; // Saved source after removal
        const string outputTargetPath = "target_updated.pdf"; // Saved target with new page

        // Page number to move (1‑based indexing)
        const int pageNumberToMove = 2;

        // Ensure input files exist
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
            using (Document srcDoc = new Document(sourcePath))
            using (Document tgtDoc = new Document(targetPath))
            {
                // Validate requested page exists in source
                if (pageNumberToMove < 1 || pageNumberToMove > srcDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumberToMove} does not exist in source document.");
                    return;
                }

                // Retrieve the page (preserves rotation, size, media box, etc.)
                Page pageToMove = srcDoc.Pages[pageNumberToMove];

                // Insert the page at the end of the target document.
                // Insert(int, Page) keeps all page properties intact.
                int insertPosition = tgtDoc.Pages.Count + 1; // append after last page
                tgtDoc.Pages.Insert(insertPosition, pageToMove);

                // Remove the page from the source document.
                srcDoc.Pages.Delete(pageNumberToMove);

                // Save the modified documents.
                srcDoc.Save(outputSourcePath);
                tgtDoc.Save(outputTargetPath);
            }

            Console.WriteLine($"Page {pageNumberToMove} moved from '{sourcePath}' to '{targetPath}'.");
            Console.WriteLine($"Updated source saved as '{outputSourcePath}'.");
            Console.WriteLine($"Updated target saved as '{outputTargetPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}