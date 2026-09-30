using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdf  = "file1.pdf";
        const string secondPdf = "file2.pdf";
        // Output merged PDF path
        const string outputPdf = "merged.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Use the two‑file overload of Concatenate to merge the PDFs
        editor.Concatenate(firstPdf, secondPdf, outputPdf);

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}