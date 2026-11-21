using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPdf = "destination.pdf";
        const string sourcePdf = "source.pdf";
        const string outputPdf = "merged_output.pdf";

        // Verify that input files exist
        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        try
        {
            // Determine the middle position of the destination PDF (0‑based index for insertion)
            int insertAfterPage;
            using (Document destDoc = new Document(destinationPdf))
            {
                // Pages are 1‑based; inserting after page 0 means prepend.
                // For an even number of pages we insert after the first half.
                insertAfterPage = destDoc.Pages.Count / 2;
            }

            // Load both documents
            Document destination = new Document(destinationPdf);
            Document source = new Document(sourcePdf);

            // Insert all pages from source after the calculated page.
            // Insert(pageNumber, sourcePages) inserts BEFORE the specified pageNumber.
            // Therefore we add 1 to insert AFTER the desired page.
            int insertionIndex = insertAfterPage + 1; // 1‑based page number where insertion starts
            destination.Pages.Insert(insertionIndex, source.Pages);

            // Save the merged document
            destination.Save(outputPdf);

            Console.WriteLine($"Pages from '{sourcePdf}' inserted into '{destinationPdf}' after page {insertAfterPage}.");
            Console.WriteLine($"Result saved as '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}
