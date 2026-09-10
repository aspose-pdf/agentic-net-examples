using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string destinationPdf = "destination.pdf";
        const string sourcePdf      = "source.pdf";
        const string outputPdf      = "merged.pdf";

        // Verify that both input files exist.
        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"File not found: {destinationPdf}");
            return;
        }
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"File not found: {sourcePdf}");
            return;
        }

        // Determine the middle insertion point (1‑based indexing).
        int insertLocation;
        using (Document destDoc = new Document(destinationPdf))
        {
            // Insert after half of the pages to place the new pages in the middle.
            insertLocation = destDoc.Pages.Count / 2 + 1;
        }

        // Determine the range of pages to insert from the source PDF.
        int startPage = 1;
        int endPage;
        using (Document srcDoc = new Document(sourcePdf))
        {
            endPage = srcDoc.Pages.Count; // Insert the whole source document.
        }

        // Perform the insertion using PdfFileEditor.
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Insert(
            destinationPdf,   // Input PDF (the one into which pages will be inserted)
            insertLocation,   // Position in the input PDF where pages are inserted
            sourcePdf,        // PDF providing the pages to insert
            startPage,        // First page of the source PDF to insert
            endPage,          // Last page of the source PDF to insert
            outputPdf);       // Output PDF containing the combined result

        if (result)
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
        else
            Console.Error.WriteLine("Failed to insert pages.");
    }
}