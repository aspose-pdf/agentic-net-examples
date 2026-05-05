using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the final concatenated PDF
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "concatenated.pdf";

        // Verify that the source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Define the page numbers (1‑based) that should be inserted.
        // Example: insert pages 2, 4 and 5 from the source PDF.
        int[] pagesToInsert = new int[] { 2, 4, 5 };

        // Position (1‑based) in the target PDF where the pages will be inserted.
        // Position = 1 inserts before the first page, Position = targetPageCount+1 appends at the end.
        // Here we insert after the first page of the original document.
        int insertPosition = 2;

        // Use PdfFileEditor to perform the insertion.
        // The Insert method signature:
        // Insert(string inputPdf, int position, string sourcePdf, int[] pages, string outputPdf)
        Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

        try
        {
            bool success = editor.Insert(sourcePdfPath, insertPosition, sourcePdfPath, pagesToInsert, outputPdfPath);
            if (success)
            {
                Console.WriteLine($"Pages {string.Join(", ", pagesToInsert)} inserted at position {insertPosition}.");
                Console.WriteLine($"Result saved to '{outputPdfPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Insert operation failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}