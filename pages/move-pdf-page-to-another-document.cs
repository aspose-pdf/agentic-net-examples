using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePath      = "source.pdf";          // PDF containing the page to move
        const string destinationPath = "destination.pdf";     // PDF that will receive the page
        const string sourceResult    = "source_modified.pdf"; // Source after page removal
        const string destResult      = "destination_with_page.pdf";

        // Validate input files
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // -----------------------------------------------------------------
        // Load source and destination documents inside using blocks (lifecycle rule)
        // -----------------------------------------------------------------
        using (Document srcDoc = new Document(sourcePath))
        using (Document dstDoc = new Document())
        {
            // -----------------------------------------------------------------
            // Choose the page to move (1‑based indexing – page 1 is the first page)
            // -----------------------------------------------------------------
            const int pageNumberToMove = 1; // change as required

            if (pageNumberToMove < 1 || pageNumberToMove > srcDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Retrieve the page object – it already carries its rotation and size
            Page pageToMove = srcDoc.Pages[pageNumberToMove];

            // -----------------------------------------------------------------
            // Insert the page into the destination document.
            // Insert(pageNumber, Page) keeps the original rotation and size.
            // -----------------------------------------------------------------
            // Insert at the end of the destination (position = Count + 1)
            int insertPosition = dstDoc.Pages.Count + 1;
            dstDoc.Pages.Insert(insertPosition, pageToMove);

            // -----------------------------------------------------------------
            // Remove the page from the source document.
            // After insertion the page object is no longer part of srcDoc,
            // but Delete ensures the source collection is consistent.
            // -----------------------------------------------------------------
            srcDoc.Pages.Delete(pageNumberToMove);
            
            // -----------------------------------------------------------------
            // Save both documents (save options are not required for PDF output)
            // -----------------------------------------------------------------
            srcDoc.Save(sourceResult);
            dstDoc.Save(destResult);
        }

        Console.WriteLine($"Page moved successfully.");
        Console.WriteLine($"Modified source saved as: {sourceResult}");
        Console.WriteLine($"Destination with moved page saved as: {destResult}");
    }
}