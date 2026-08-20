using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPdf = "destination.pdf"; // PDF into which pages will be inserted
        const string sourcePdf      = "source.pdf";      // PDF providing the pages to insert
        const string outputPdf      = "output.pdf";      // Resulting PDF after insertion

        // Pages to take from the source PDF (1‑based indexing)
        int[] pagesToInsert = new int[] { 2, 4, 6 };

        // Position in the destination PDF where the pages will be inserted.
        // 1 = before the first page, 2 = after the first page, etc.
        int insertLocation = 3;

        // Basic validation of input files
        if (!System.IO.File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }
        if (!System.IO.File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Perform the insertion using Aspose.Pdf.Facades.PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Insert(destinationPdf, insertLocation, sourcePdf, pagesToInsert, outputPdf);

        if (result)
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
        else
            Console.Error.WriteLine("Failed to insert pages.");
    }
}