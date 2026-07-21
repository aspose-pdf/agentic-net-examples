using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPdf = "destination.pdf"; // PDF into which pages will be inserted
        const string sourcePdf      = "source.pdf";      // PDF providing pages to insert
        const string outputPdf      = "merged.pdf";      // Resulting PDF after insertion

        // Position (1‑based) in the destination PDF where pages will be inserted.
        // For example, 1 inserts before the first page, 3 inserts after page 2, etc.
        int insertPosition = 3;

        // Specific pages from the source PDF to insert (1‑based page numbers).
        int[] pagesToInsert = new int[] { 2, 5, 7 };

        // Validate that the files exist before proceeding.
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
            // PdfFileEditor does NOT implement IDisposable, so no using block is required.
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the selected pages from sourcePdf into destinationPdf at the specified position.
            // This uses the overload: Insert(string inputFile, int insertLocation,
            //                               string portFile, int[] pageNumber, string outputFile)
            bool success = editor.Insert(
                destinationPdf,          // inputFile – the PDF that will receive the pages
                insertPosition,         // insertLocation – where to insert (1‑based)
                sourcePdf,              // portFile – the PDF providing pages
                pagesToInsert,          // pageNumber – array of pages to take from sourcePdf
                outputPdf);             // outputFile – resulting PDF

            if (success)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insertion failed. The operation returned false.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., file access issues, invalid page numbers).
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}