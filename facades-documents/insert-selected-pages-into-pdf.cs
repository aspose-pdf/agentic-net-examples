using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (pages to take), the original destination PDF,
        // and the final PDF that will contain the inserted pages.
        const string sourcePath = "source.pdf";
        const string destinationPath = "destination.pdf";
        const string outputPath = "result.pdf";

        // Define which pages (1‑based) from the source PDF should be inserted.
        int[] pagesToInsert = new int[] { 2, 4, 5 };

        // Position in the destination PDF after which the pages will be inserted.
        // Use 0 to insert at the very beginning.
        int insertAfterPage = 3;

        // Verify that the input files exist.
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

        // Load both PDFs using the Document class (PdfFileEditor does not provide InsertPage).
        Document srcDoc = new Document(sourcePath);
        Document destDoc = new Document(destinationPath);

        // Determine the insertion index for the destination document.
        // Aspose.Pdf.Pages.Insert inserts *before* the supplied index (1‑based).
        // To insert after page N we insert at position N + 1. If N == 0 we insert at the very start (position 1).
        int insertPos = insertAfterPage == 0 ? 1 : insertAfterPage + 1;

        // Insert the selected pages preserving their original order.
        foreach (int srcPageNumber in pagesToInsert)
        {
            // Validate source page number.
            if (srcPageNumber < 1 || srcPageNumber > srcDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Source page {srcPageNumber} is out of range. Skipping.");
                continue;
            }

            // Clone the page from the source document.
            Page pageToInsert = srcDoc.Pages[srcPageNumber];
            // Insert the page into the destination document at the calculated position.
            destDoc.Pages.Insert(insertPos, pageToInsert);
            // Advance the insertion position so subsequent pages keep their order.
            insertPos++;
        }

        // Save the modified document to the output path.
        destDoc.Save(outputPath);

        Console.WriteLine($"Inserted pages {string.Join(",", pagesToInsert)} from '{sourcePath}' into '{destinationPath}' after page {insertAfterPage}.");
        Console.WriteLine($"Result saved as '{outputPath}'.");
    }
}
