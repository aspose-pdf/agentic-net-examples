using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";          // source document
        const string outputPdf = "extracted.pdf";      // result document
        const int endPage = 5;                         // extract pages 1..5

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // SplitFromFirst extracts pages from the first page up to endPage (inclusive)
            // Signature: SplitFromFirst(string sourceFile, int endPage, string outputFile)
            editor.SplitFromFirst(sourcePdf, endPage, outputPdf);

            Console.WriteLine($"Pages 1-{endPage} saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}
