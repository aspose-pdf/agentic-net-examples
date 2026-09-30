using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the destination PDF, a source PDF to insert, and the resulting output PDF
        const string destinationPdfPath = "destination.pdf";
        const string sourcePdfPath      = "source.pdf";
        const string outputPdfPath      = "merged.pdf";

        // Verify that the required files exist
        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Destination PDF not found: {destinationPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        try
        {
            // Use Document to obtain the page count of the source PDF (PdfFileEditor has no GetPageCount in this version)
            Document srcDoc = new Document(sourcePdfPath);
            int sourcePageCount = srcDoc.Pages.Count;

            // Build an array with all page numbers that should be inserted
            int[] pagesToInsert = Enumerable.Range(1, sourcePageCount).ToArray();

            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the source PDF into the destination PDF.
            // Signature: Insert(string sourceFile, int startPage, string destinationFile, int[] pages, string outputFile)
            // startPage = 1 means insertion at the beginning of the destination PDF.
            editor.Insert(sourcePdfPath, 1, destinationPdfPath, pagesToInsert, outputPdfPath);

            Console.WriteLine($"Inserted '{sourcePdfPath}' into '{destinationPdfPath}' and saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
