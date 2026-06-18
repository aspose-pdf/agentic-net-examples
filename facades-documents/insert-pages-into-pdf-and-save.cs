using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the PDF whose pages will be inserted,
        // and the resulting PDF after insertion.
        const string sourcePdf = "source.pdf";
        const string insertPdf = "insert.pdf";
        const string outputPdf = "output.pdf";

        // Position (1‑based) in the source PDF where the new pages will be inserted.
        // For example, 2 means the pages will be inserted after the first page.
        int insertLocation = 2;

        // Page numbers (1‑based) from the insertPdf that should be inserted.
        int[] pagesToInsert = new int[] { 1, 3 };

        // Verify that the input files exist before proceeding.
        if (!File.Exists(sourcePdf) || !File.Exists(insertPdf))
        {
            Console.Error.WriteLine("Source or insert PDF file not found.");
            return;
        }

        // PdfFileEditor is a facade for page‑level operations such as insertion.
        // It does NOT implement IDisposable, so no using block is required.
        PdfFileEditor editor = new PdfFileEditor();

        // TryInsert performs the insertion and writes the result directly to outputPdf.
        // It returns true on success, false otherwise.
        bool result = editor.TryInsert(sourcePdf, insertLocation, insertPdf, pagesToInsert, outputPdf);

        if (result)
        {
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to insert pages into the PDF.");
        }
    }
}