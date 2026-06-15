using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPdf = "destination.pdf";   // PDF into which pages will be inserted
        const string sourcePdf      = "source.pdf";        // PDF providing pages to insert
        const string outputPdf      = "merged.pdf";        // Resulting PDF after insertion

        // Insertion parameters
        // InsertLocation: position in the destination PDF where pages will be inserted (1‑based)
        // StartPage / EndPage: range of pages (inclusive) from the source PDF to insert
        const int insertLocation = 2;   // e.g., insert after the first page of destination
        const int startPage      = 3;   // first page from source to insert
        const int endPage        = 5;   // last page from source to insert

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

            // Insert the specified page range from sourcePdf into destinationPdf.
            // The method returns true on success.
            bool success = editor.Insert(
                destinationPdf,   // inputFile – the PDF to receive the pages
                insertLocation,   // insertLocation – where to insert (1‑based)
                sourcePdf,        // portFile – the PDF providing pages
                startPage,        // startPage – first page to take from source
                endPage,          // endPage – last page to take from source
                outputPdf);       // outputFile – resulting PDF

            if (success)
                Console.WriteLine($"Pages {startPage}-{endPage} from '{sourcePdf}' inserted into '{destinationPdf}' at position {insertLocation}. Result saved as '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insertion failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}