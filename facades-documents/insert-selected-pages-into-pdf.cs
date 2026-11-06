using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (pages to take) and the destination PDF (where to insert)
        const string sourcePdf = "source.pdf";
        const string destinationPdf = "dest.pdf";
        const string outputPdf = "output.pdf";

        // Array of page numbers (1‑based) to insert from the source PDF
        int[] pagesToInsert = new int[] { 2, 4, 5 };

        // Position in the destination PDF after which the pages will be inserted.
        // 1‑based index; insert after this page number.
        int insertPosition = 3;

        // Validate files exist
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }

        // Use PdfFileEditor to insert the selected pages.
        // The Insert method writes the result directly to the output file.
        PdfFileEditor editor = new PdfFileEditor();
        try
        {
            // Insert after 'insertPosition' page of the destination PDF.
            editor.Insert(destinationPdf, insertPosition, sourcePdf, pagesToInsert, outputPdf);
            Console.WriteLine($"Successfully inserted {pagesToInsert.Length} pages into '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}
