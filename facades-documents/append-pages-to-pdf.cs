using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string destinationPdf = "destination.pdf"; // existing PDF to receive pages
        const string sourcePdf      = "source.pdf";      // PDF whose pages will be appended
        const string outputPdf      = "merged.pdf";      // result PDF

        // Verify input files exist
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

        // Determine the total number of pages in the source PDF
        int sourcePageCount;
        using (Document srcDoc = new Document(sourcePdf))
        {
            sourcePageCount = srcDoc.Pages.Count;
        }

        // Append all pages from sourcePdf to the end of destinationPdf
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Append(
            inputFile:  destinationPdf,
            portFile:   sourcePdf,
            startPage:  1,
            endPage:    sourcePageCount,
            outputFile: outputPdf);

        if (success)
        {
            Console.WriteLine($"Pages from '{sourcePdf}' successfully appended to '{destinationPdf}'.");
            Console.WriteLine($"Result saved as '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Append operation failed.");
        }
    }
}