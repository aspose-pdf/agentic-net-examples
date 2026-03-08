using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output PDF and the insertion index (1‑based)
        const string sourcePath = "source.pdf";
        const string outputPath = "output.pdf";
        const int insertIndex = 3; // Insert before page 3

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create a temporary PDF that contains a single blank page.
        // This file will be used as the source of the page to insert.
        string tempBlankPdf = Path.ChangeExtension(Path.GetTempFileName(), ".pdf");

        try
        {
            // Build the blank PDF.
            using (Document blankDoc = new Document())
            {
                // Add an empty page (default size).
                blankDoc.Pages.Add();
                blankDoc.Save(tempBlankPdf);
            }

            // Use PdfFileEditor (Facades API) to insert the blank page.
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap it in a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // InsertLocation is 1‑based. The pageNumber array specifies which pages
            // from the temporary PDF to insert (here only page 1).
            bool inserted = editor.Insert(
                sourcePath,          // input PDF
                insertIndex,         // position where the new page will be placed
                tempBlankPdf,        // PDF that provides the page to insert
                new int[] { 1 },     // page numbers from the source PDF (page 1)
                outputPath);         // resulting PDF

            if (!inserted)
            {
                Console.Error.WriteLine("Failed to insert the page.");
                return;
            }

            Console.WriteLine($"Page inserted successfully. Output saved to '{outputPath}'.");
        }
        finally
        {
            // Clean up the temporary blank PDF.
            if (File.Exists(tempBlankPdf))
            {
                try { File.Delete(tempBlankPdf); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}