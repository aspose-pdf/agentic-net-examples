using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the PDF containing pages to insert, and the output PDF
        string sourcePdf = "source.pdf";
        string insertPdf = "insert.pdf";
        string outputPdf = "result.pdf";

        // Define which pages from insertPdf should be inserted (1‑based page numbers)
        int[] pagesToInsert = new int[] { 1, 2 };

        // Define the position in sourcePdf where the pages will be inserted (1‑based)
        int insertLocation = 1;

        // Use PdfFileEditor to perform the insertion.
        // TryInsert writes the modified document directly to the output file.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.TryInsert(sourcePdf, insertLocation, insertPdf, pagesToInsert, outputPdf);

        // Report the result.
        if (success)
        {
            Console.WriteLine($"Insertion succeeded. Output saved to '{outputPdf}'.");
        }
        else
        {
            Console.WriteLine("Insertion failed.");
        }
    }
}