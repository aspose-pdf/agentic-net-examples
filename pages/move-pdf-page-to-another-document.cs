using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";   // PDF containing the page to move
        const string targetPath = "target.pdf";   // PDF that will receive the page
        const string outputPath = "result.pdf";   // Final PDF after the move
        const int pageToMove = 2;                 // 1‑based page number to move

        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // Both documents must stay alive until Save() completes (rule: document-disposal-with-using)
        using (Document target = new Document(targetPath))
        using (Document source = new Document(sourcePath))
        {
            // Validate the requested page index (pages are 1‑based)
            if (pageToMove < 1 || pageToMove > source.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Add the page to the target document.
            // This copies the page together with its original rotation, size, and other attributes.
            target.Pages.Add(source.Pages[pageToMove]);

            // Remove the page from the source document.
            source.Pages.Delete(pageToMove);

            // Save the modified target document (the moved page is now part of it).
            target.Save(outputPath);
        }

        Console.WriteLine($"Page {pageToMove} moved to '{outputPath}'.");
    }
}