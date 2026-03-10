using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the original PDF, the PDF containing pages to insert, and the output PDF.
        const string sourcePdf = "source.pdf";
        const string insertPdf = "insert.pdf";
        const string outputPdf = "result.pdf";

        // Position (1‑based) in the source PDF where the new pages will be inserted.
        // For example, 2 means the pages will be inserted after the first page.
        int insertPosition = 2;

        // Range of pages from the insert PDF to be inserted (inclusive, 1‑based).
        int startPage = 1;
        int endPage   = 3;

        // Verify that the input files exist before proceeding.
        if (!File.Exists(sourcePdf) || !File.Exists(insertPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // PdfFileEditor provides the Insert method which loads the PDFs,
            // inserts the specified pages, and saves the result to outputPdf.
            PdfFileEditor editor = new PdfFileEditor();
            bool result = editor.Insert(sourcePdf, insertPosition, insertPdf, startPage, endPage, outputPdf);

            Console.WriteLine(result
                ? $"Pages inserted successfully. Output saved to '{outputPdf}'."
                : "Insert operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}