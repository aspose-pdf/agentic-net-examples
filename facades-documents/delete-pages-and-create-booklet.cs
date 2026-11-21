using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";
        const string afterDeletePdf = "temp_deleted.pdf";
        const string bookletPdf = "booklet.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Pages to delete (1‑based indexing). Adjust as needed.
        int[] pagesToDelete = new int[] { 2, 5, 7 };

        try
        {
            // ------------------------------------------------------------
            // 1. Delete unwanted pages using the Document API.
            // ------------------------------------------------------------
            Document doc = new Document(sourcePdf);

            // Delete in descending order so that earlier deletions do not shift
            // the indices of pages that are still to be removed.
            Array.Sort(pagesToDelete);
            for (int i = pagesToDelete.Length - 1; i >= 0; i--)
            {
                int pageNum = pagesToDelete[i];
                if (pageNum >= 1 && pageNum <= doc.Pages.Count)
                {
                    doc.Pages.Delete(pageNum);
                }
            }
            doc.Save(afterDeletePdf);
            Console.WriteLine($"Deleted pages {string.Join(",", pagesToDelete)} -> {afterDeletePdf}");

            // ------------------------------------------------------------
            // 2. Generate a booklet from the cleaned PDF.
            // ------------------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(afterDeletePdf, bookletPdf); // overload without PageSize
            Console.WriteLine(success ? $"Booklet created: {bookletPdf}" : "Booklet creation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file.
            if (File.Exists(afterDeletePdf))
            {
                try { File.Delete(afterDeletePdf); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}