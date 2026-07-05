using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the PDF containing pages to insert, and the result PDF
        const string sourcePdf   = "source.pdf";
        const string insertPdf   = "insert.pdf";
        const string outputPdf   = "output.pdf";

        // Position (1‑based) where the new pages will be inserted in the source PDF
        const int insertLocation = 2; // after the first page

        // Page numbers (1‑based) from insertPdf that should be inserted
        int[] pagesToInsert = new int[] { 1, 3 };

        // Verify that the input files exist
        if (!File.Exists(sourcePdf) || !File.Exists(insertPdf))
        {
            Console.Error.WriteLine("Source or insert PDF file not found.");
            return;
        }

        // Use PdfFileEditor (a Facade) to perform the insertion.
        // TryInsert writes the modified document directly to the output path.
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.TryInsert(sourcePdf, insertLocation, insertPdf, pagesToInsert, outputPdf);

        if (result)
        {
            Console.WriteLine($"Pages inserted successfully. Saved to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to insert pages.");
        }
    }
}