using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the PDF containing the page(s) to insert,
        // and the resulting PDF after insertion.
        const string sourcePdf   = "source.pdf";
        const string insertPdf   = "insert.pdf";
        const string outputPdf   = "output.pdf";

        // Verify that the input files exist.
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(insertPdf))
        {
            Console.Error.WriteLine($"Insert file not found: {insertPdf}");
            return;
        }

        // Position at which to insert pages (1‑based index).
        // For example, 2 means the new pages will be placed after the first page.
        int insertLocation = 2;

        // Define the range of pages from the insert PDF to be inserted.
        // Here we insert only the first page (startPage = 1, endPage = 1).
        int startPage = 1;
        int endPage   = 1;

        // Perform the insertion using PdfFileEditor.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Insert(sourcePdf, insertLocation, insertPdf, startPage, endPage, outputPdf);

        if (success)
        {
            Console.WriteLine($"Page inserted successfully. Output saved to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to insert page.");
        }
    }
}