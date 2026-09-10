using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the PDF whose pages will be inserted, and the output PDF
        const string sourcePdfPath = "source.pdf";
        const string insertPdfPath = "insert.pdf";
        const string outputPdfPath = "output.pdf";

        // Position (1‑based) in the source PDF where the new pages will be inserted
        int insertLocation = 2; // after the first page

        // Page numbers (1‑based) from the insert PDF that should be inserted
        int[] pagesToInsert = new int[] { 1, 3 };

        // Create the facade that handles page insertion
        PdfFileEditor editor = new PdfFileEditor();

        // TryInsert performs the insertion and writes the result directly to the output file
        bool result = editor.TryInsert(
            sourcePdfPath,      // input PDF
            insertLocation,     // insertion point
            insertPdfPath,      // PDF providing pages to insert
            pagesToInsert,      // pages to take from the insert PDF
            outputPdfPath);     // destination file

        // Report the outcome
        Console.WriteLine(result
            ? $"Insertion succeeded. Modified PDF saved to '{outputPdfPath}'."
            : "Insertion failed.");
    }
}