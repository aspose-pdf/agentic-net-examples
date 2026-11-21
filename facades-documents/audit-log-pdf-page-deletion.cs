using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf1 = "output_step1.pdf";
        const string outputPdf2 = "output_step2.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // First delete operation: remove pages 2 and 3 (1‑based indexing)
        int[] pagesToDelete1 = new int[] { 2, 3 };
        DeletePagesWithAudit(inputPdf, outputPdf1, pagesToDelete1);

        // Second delete operation: remove page 5 from the result of the first deletion
        int[] pagesToDelete2 = new int[] { 5 };
        DeletePagesWithAudit(outputPdf1, outputPdf2, pagesToDelete2);
    }

    static void DeletePagesWithAudit(string sourcePath, string destPath, int[] pagesToDelete)
    {
        // Log how many pages are being removed in this operation
        Console.WriteLine($"Deleting {pagesToDelete.Length} page(s) from '{sourcePath}' to '{destPath}'.");

        // Perform the deletion using PdfFileEditor (non‑disposable)
        PdfFileEditor editor = new PdfFileEditor();
        // NOTE: PdfFileEditor.Delete expects the order (source, pagesToDelete, destination)
        editor.Delete(sourcePath, pagesToDelete, destPath);

        // Optional verification: report the page count of the resulting document
        using (Document resultDoc = new Document(destPath))
        {
            Console.WriteLine($"Resulting document '{destPath}' contains {resultDoc.Pages.Count} page(s).");
        }
    }
}
