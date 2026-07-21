using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the destination PDF, the source PDF (pages to insert), and the output PDF.
        const string destinationPdf = "dest.pdf";
        const string sourcePdf      = "source.pdf";
        const string outputPdf      = "merged.pdf";

        // Insert location in the destination PDF (1‑based index).
        // For example, 2 means the pages will be inserted after the first page.
        int insertLocation = 2;

        // Range of pages to take from the source PDF (inclusive, 1‑based).
        int startPage = 3;
        int endPage   = 5;

        // Verify that the input files exist.
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

        // Use PdfFileEditor (Aspose.Pdf.Facades) to perform the insertion.
        PdfFileEditor editor = new PdfFileEditor();

        // Insert the specified page range from sourcePdf into destinationPdf at insertLocation.
        // The result is saved to outputPdf.
        bool success = editor.Insert(
            destinationPdf,   // inputFile – the PDF to receive the pages
            insertLocation,  // insertLocation – position in the input file (1‑based)
            sourcePdf,       // portFile – the PDF providing pages
            startPage,       // startPage – first page to insert from portFile
            endPage,         // endPage – last page to insert from portFile
            outputPdf);      // outputFile – resulting PDF

        if (success)
        {
            Console.WriteLine($"Successfully inserted pages {startPage}-{endPage} from '{sourcePdf}' into '{destinationPdf}' at position {insertLocation}.");
            Console.WriteLine($"Result saved as '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Insert operation failed.");
        }
    }
}