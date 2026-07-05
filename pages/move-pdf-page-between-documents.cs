using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string sourcePath      = "source.pdf";   // PDF containing the page to move
        const string destinationPath = "target.pdf";   // PDF that will receive the page
        const string updatedSource   = "source_updated.pdf";
        const string updatedTarget   = "target_updated.pdf";

        const int pageToMove = 2; // 1‑based index of the page to move from source

        // Validate input files
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }

        try
        {
            // Open both documents inside using blocks for deterministic disposal
            using (Document srcDoc = new Document(sourcePath))
            using (Document dstDoc = new Document(destinationPath))
            {
                // Ensure the requested page exists
                if (pageToMove < 1 || pageToMove > srcDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageToMove} does not exist in source document.");
                    return;
                }

                // Retrieve the page object (rotation, size, etc. are part of the Page instance)
                Page page = srcDoc.Pages[pageToMove];

                // Add the page to the destination document.
                // Add() appends to the end; the page retains its original Rotate, MediaBox, CropBox, etc.
                dstDoc.Pages.Add(page);

                // Remove the page from the source document.
                srcDoc.Pages.Delete(pageToMove);

                // Save the modified documents.
                srcDoc.Save(updatedSource);
                dstDoc.Save(updatedTarget);
            }

            Console.WriteLine($"Page {pageToMove} moved from '{sourcePath}' to '{destinationPath}'.");
            Console.WriteLine($"Updated source saved as '{updatedSource}'.");
            Console.WriteLine($"Updated destination saved as '{updatedTarget}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}