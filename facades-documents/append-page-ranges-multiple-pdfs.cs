using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base PDF into which pages will be inserted/appended
        const string basePdf = "base.pdf";

        // Source PDFs from which pages will be taken
        string[] sourcePdfs = { "source1.pdf", "source2.pdf", "source3.pdf" };

        // Page range (inclusive) to take from each source PDF
        const int startPage = 2; // first page to include
        const int endPage   = 5; // last page to include

        // Output PDF file
        const string outputPdf = "merged_output.pdf";

        // Verify that all files exist before proceeding
        if (!File.Exists(basePdf))
        {
            Console.Error.WriteLine($"Base file not found: {basePdf}");
            return;
        }

        foreach (string src in sourcePdfs)
        {
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"Source file not found: {src}");
                return;
            }
        }

        // Perform the append operation in a single call
        // TryAppend adds pages from each source PDF (portFiles) within the same page range.
        // It returns false instead of throwing an exception if something goes wrong.
        PdfFileEditor editor = new PdfFileEditor();

        bool success = editor.TryAppend(basePdf, sourcePdfs, startPage, endPage, outputPdf);

        if (success)
        {
            Console.WriteLine($"Pages {startPage}-{endPage} from each source PDF have been appended to '{basePdf}'.");
            Console.WriteLine($"Result saved as '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to append pages. See editor.LastException for details if needed.");
        }
    }
}