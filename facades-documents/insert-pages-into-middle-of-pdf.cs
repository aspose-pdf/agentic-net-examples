using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string destinationPath = "destination.pdf";
        const string sourcePath      = "source.pdf";
        const string outputPath      = "merged_output.pdf";

        // Verify input files exist
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Determine the middle position of the destination PDF (1‑based indexing)
        int insertPosition;
        using (Document destDoc = new Document(destinationPath))
        {
            int pageCount = destDoc.Pages.Count;
            // Insert after the first half of pages
            insertPosition = (pageCount / 2) + 1;
        }

        // Load both PDFs
        Document destinationDoc = new Document(destinationPath);
        Document sourceDoc      = new Document(sourcePath);

        // Insert all pages from source PDF into destination PDF at the calculated position
        // Pages.Insert takes the position (1‑based) and a PageCollection to insert.
        destinationDoc.Pages.Insert(insertPosition, sourceDoc.Pages);

        // Save the merged document
        destinationDoc.Save(outputPath);

        Console.WriteLine($"Pages from '{sourcePath}' inserted into '{destinationPath}' at position {insertPosition}.");
        Console.WriteLine($"Result saved as '{outputPath}'.");
    }
}
