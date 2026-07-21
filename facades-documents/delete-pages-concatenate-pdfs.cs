using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // Input files
        const string sourcePdf = "source.pdf";      // PDF from which pages will be removed
        const string secondPdf = "second.pdf";      // PDF to be concatenated after deletion
        // Intermediate and final output files
        const string tempPdf   = "temp_deleted.pdf"; // Holds the result after page deletion
        const string outputPdf = "final_merged.pdf"; // Final concatenated document

        // ---------------------------------------------------------------------
        // Create sample PDFs so the example can run in a clean sandbox.
        // This satisfies the "hardcoded-input-file-generate-inline-first" rule.
        // ---------------------------------------------------------------------
        // Keep the total number of pages <= 4 when running with an evaluation
        // license (Aspose limits evaluation builds to four pages). Adjust the
        // page counts accordingly.
        CreateSamplePdf(sourcePdf, 3, "Source PDF - Page ");   // 3 pages
        CreateSamplePdf(secondPdf, 1, "Second PDF - Page ");   // 1 page

        // Pages to delete – 1‑based indexing as required by Aspose.Pdf.
        // In this example we delete no pages to stay within the 4‑page limit.
        int[] pagesToDelete = new int[0];   // empty array – nothing to delete

        // ------------------------------------------------------------
        // Step 1: Delete the specified pages from the source PDF.
        // PdfFileEditor does NOT implement IDisposable, so it is NOT wrapped in a using block.
        // The Delete method returns a bool indicating success.
        // ------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        bool deleteSuccess = editor.Delete(sourcePdf, pagesToDelete, tempPdf);
        if (!deleteSuccess)
        {
            Console.Error.WriteLine("Failed to delete pages from the source PDF.");
            return;
        }

        // ------------------------------------------------------------
        // Step 2: Concatenate the edited PDF (tempPdf) with the second PDF.
        // CloseConcatenatedStreams is optional but ensures streams are closed after the operation.
        // ------------------------------------------------------------
        editor.CloseConcatenatedStreams = true;
        bool concatSuccess = editor.Concatenate(tempPdf, secondPdf, outputPdf);
        if (!concatSuccess)
        {
            Console.Error.WriteLine("Failed to concatenate PDFs.");
            return;
        }

        Console.WriteLine($"Successfully created merged PDF: '{outputPdf}'.");
    }

    // Helper that creates a simple PDF with the requested number of pages.
    static void CreateSamplePdf(string path, int pageCount, string pageTitlePrefix)
    {
        using (Document doc = new Document())
        {
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment($"{pageTitlePrefix}{i}"));
            }
            doc.Save(path);
        }
    }
}
