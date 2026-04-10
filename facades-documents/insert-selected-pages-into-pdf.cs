using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPdf = "destination.pdf"; // file into which pages will be inserted
        const string sourcePdf      = "source.pdf";      // file providing pages to insert
        const string outputPdf      = "merged.pdf";      // result file

        // Pages to take from sourcePdf (1‑based indexing)
        int[] pagesToInsert = new int[] { 2, 4, 5 };

        // Position in destinationPdf where the pages will be inserted (1‑based)
        // Example: 1 = before the first page, 3 = after the second page, etc.
        int insertPosition = 3;

        // Validate input files
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

            // Insert the selected pages from sourcePdf into destinationPdf.
            // Overload: Insert(string inputFile, int insertLocation,
            //                 string portFile, int[] pageNumber, string outputFile)
            bool success = editor.Insert(
                destinationPdf,          // inputFile – the PDF that will receive the pages
                insertPosition,         // insertLocation – where to insert (1‑based)
                sourcePdf,              // portFile – the PDF providing pages
                pagesToInsert,          // pageNumber – array of pages to copy from portFile
                outputPdf);             // outputFile – resulting PDF

            if (success)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}