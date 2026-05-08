using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string changedPath = "changed_page_size.pdf";
        const string revertedPath = "reverted_page_size.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // First edit: change page size to a custom dimension
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Store original size of the first page (pages are 1‑based)
            PageSize originalSize = editor.GetPageSize(1);

            // Set a new custom page size (width = 500 points, height = 800 points)
            editor.PageSize = new PageSize(500f, 800f);

            // Apply the change and save the result
            editor.ApplyChanges();
            editor.Save(changedPath);

            // ---- Undo test: revert to the original size ----

            // Re‑bind the PDF that was just saved with the custom size
            editor.BindPdf(changedPath);

            // Restore the original page dimensions
            editor.PageSize = new PageSize(originalSize.Width, originalSize.Height);

            // Apply the revert and save to a new file
            editor.ApplyChanges();
            editor.Save(revertedPath);
        }

        Console.WriteLine($"Custom size saved to '{changedPath}'.");
        Console.WriteLine($"Reverted size saved to '{revertedPath}'.");
    }
}