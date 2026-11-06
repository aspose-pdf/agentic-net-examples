using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";
        const string outputPdf = "split_from_page5.pdf";
        const int startPage = 5; // 1‑based page index

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Split from startPage to the end of the document and save to outputPdf
            // NOTE: SplitToEnd signature is (string sourceFile, int startPage, string outputFile)
            editor.SplitToEnd(sourcePdf, startPage, outputPdf);

            Console.WriteLine($"PDF split saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}
