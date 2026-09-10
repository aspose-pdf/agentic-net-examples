using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPdf = "destination.pdf"; // PDF into which pages will be inserted
        const string sourcePdf      = "source.pdf";      // PDF providing pages to insert
        const string outputPdf      = "merged.pdf";      // Resulting PDF after insertion

        // Insertion parameters
        const int insertLocation = 2; // Position in destination PDF where pages will be inserted (1‑based)
        const int startPage      = 3; // First page in source PDF to insert (1‑based)
        const int endPage        = 5; // Last page in source PDF to insert (inclusive)

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
            // PdfFileEditor provides the Insert method for page range insertion.
            PdfFileEditor editor = new PdfFileEditor();

            // Insert pages from sourcePdf (startPage..endPage) into destinationPdf at insertLocation.
            // The method returns true on success.
            bool success = editor.Insert(
                destinationPdf,   // inputFile – the PDF that will receive the pages
                insertLocation,  // insertLocation – where to insert (1‑based index)
                sourcePdf,       // portFile – the PDF providing pages
                startPage,       // startPage – first page to take from sourcePdf
                endPage,         // endPage – last page to take from sourcePdf
                outputPdf);      // outputFile – resulting PDF

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