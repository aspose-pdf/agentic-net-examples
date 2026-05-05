using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        // Paths to the destination PDF, source PDF and the resulting PDF
        const string destinationPdf = "destination.pdf";
        const string sourcePdf      = "source.pdf";
        const string outputPdf      = "merged.pdf";

        // Position in the destination PDF where pages will be inserted (1‑based index)
        // For example, 3 means pages will be inserted before the original page 3.
        const int insertPosition = 3;

        // Range of pages from the source PDF to insert (inclusive, 1‑based)
        const int sourceStartPage = 2;
        const int sourceEndPage   = 5;

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

        // PdfFileEditor does NOT implement IDisposable, so instantiate it directly.
        var editor = new PdfFileEditor();
        try
        {
            // Insert pages from sourcePdf (pages sourceStartPage‑sourceEndPage)
            // into destinationPdf at insertPosition, saving the result to outputPdf.
            bool success = editor.Insert(
                destinationPdf,   // inputFile – the PDF into which pages will be inserted
                insertPosition,  // insertLocation – position in the input file (1‑based)
                sourcePdf,        // portFile – the PDF providing pages to insert
                sourceStartPage,  // startPage – first page to take from sourcePdf
                sourceEndPage,    // endPage – last page to take from sourcePdf
                outputPdf);       // outputFile – resulting PDF

            if (success)
                Console.WriteLine($"Pages {sourceStartPage}-{sourceEndPage} from '{sourcePdf}' inserted at position {insertPosition} in '{destinationPdf}'. Result saved as '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}
