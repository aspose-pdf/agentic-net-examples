using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap the Document in a using block for deterministic disposal (see document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing (see page-indexing-one-based rule)
            // Move page 9 to position 1 using Insert/Delete (see move-page-using-insert-delete rule)
            int sourceIndex = 9;   // original page number (1‑based)
            int targetIndex = 1;   // desired new position (1‑based)

            // Keep a reference to the page that will be moved
            Page pageToMove = doc.Pages[sourceIndex];

            // Insert the page at the target position
            doc.Pages.Insert(targetIndex, pageToMove);

            // Delete the original occurrence of the page (adjusted after insertion)
            int deleteIndex = sourceIndex >= targetIndex ? sourceIndex + 1 : sourceIndex;
            doc.Pages.Delete(deleteIndex);

            // Save the modified PDF (Document.Save without extra options writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 9 moved to first position and saved as '{outputPath}'.");
    }
}
