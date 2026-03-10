using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string targetPdf = "target.pdf";   // PDF that will receive the pages
        const string sourcePdf = "source.pdf";   // PDF containing pages to insert
        const string outputPdf = "merged.pdf";   // Resulting PDF
        const int insertAfterPage = 2;           // Insert after this page in targetPdf (1‑based)

        // Verify input files exist
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdf}");
            return;
        }
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        try
        {
            // Determine all page numbers of the source PDF
            int[] sourcePages;
            using (Document srcDoc = new Document(sourcePdf))
            {
                sourcePages = new int[srcDoc.Pages.Count];
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    sourcePages[i - 1] = i; // 1‑based page numbers
                }
            }

            // PdfFileEditor (Facade) performs the insertion.
            // It loads the PDFs internally; no explicit BindPdf is required.
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the selected pages from sourcePdf into targetPdf.
            // Overload: Insert(string srcFile, int insertAtPage, string pagesFile, int[] pages, string outputFile)
            editor.Insert(targetPdf, insertAfterPage, sourcePdf, sourcePages, outputPdf);

            Console.WriteLine($"Pages from '{sourcePdf}' inserted after page {insertAfterPage} of '{targetPdf}'.");
            Console.WriteLine($"Result saved as '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}