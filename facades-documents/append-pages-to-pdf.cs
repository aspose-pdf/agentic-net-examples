using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string destinationPdf = "destination.pdf"; // PDF that will receive the pages
        const string sourcePdf      = "source.pdf";      // PDF whose pages will be appended
        const string outputPdf      = "merged.pdf";      // Resulting PDF

        // Verify files exist
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

        // Determine the page range to copy from the source PDF.
        // Use a Document in a using block to obtain the page count.
        int startPage = 1;
        int endPage;
        using (Document srcDoc = new Document(sourcePdf))
        {
            endPage = srcDoc.Pages.Count; // Append all pages from the source
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Append the selected page range from sourcePdf to the end of destinationPdf.
        // NOTE: The correct parameter order for Append is (sourceFile, destinationFile, startPage, endPage, outputFile).
        // In recent Aspose.Pdf versions Append returns void; it throws on failure.
        editor.Append(sourcePdf, destinationPdf, startPage, endPage, outputPdf);

        Console.WriteLine($"Pages from '{sourcePdf}' (pages {startPage}-{endPage}) appended to '{destinationPdf}'.");
        Console.WriteLine($"Result saved as '{outputPdf}'.");
    }
}
