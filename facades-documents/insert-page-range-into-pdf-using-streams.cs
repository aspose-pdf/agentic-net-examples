using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source (destination) PDF, the PDF providing pages to insert,
        // and the resulting PDF.
        const string destinationPdfPath = "destination.pdf";
        const string sourcePdfPath      = "source.pdf";
        const string outputPdfPath      = "merged.pdf";

        // Page numbers are 1‑based.
        // Insert after this page in the destination PDF.
        int insertLocation = 2;   // e.g., after page 2
        // Range of pages to take from the source PDF.
        int startPage = 3;        // first page to insert
        int endPage   = 5;        // last page to insert (inclusive)

        // Validate input files exist.
        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Open streams for the three files.
        // The output stream is created with FileMode.Create to overwrite any existing file.
        using (FileStream destStream = new FileStream(destinationPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream srcStream  = new FileStream(sourcePdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream outStream  = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor provides the Insert operation for streams.
            PdfFileEditor editor = new PdfFileEditor();

            // Perform the insertion.
            bool success = editor.Insert(
                destStream,          // input PDF (destination)
                insertLocation,      // position in destination after which pages are inserted
                srcStream,           // PDF providing pages to insert
                startPage,           // first page in source to insert
                endPage,             // last page in source to insert
                outStream);          // output PDF containing the merged result

            if (success)
                Console.WriteLine($"Pages {startPage}-{endPage} inserted into '{destinationPdfPath}' at position {insertLocation}. Result saved as '{outputPdfPath}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
    }
}