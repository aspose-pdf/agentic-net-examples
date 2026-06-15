using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string resizedPath = "resized.pdf";
        const string revertedPath = "reverted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Variables to hold the original page dimensions (float because PageSize ctor expects float)
        float originalWidth;
        float originalHeight;

        // ---------- Resize to custom dimensions ----------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Retrieve the size of the first page (pages are 1‑based)
            Aspose.Pdf.PageSize originalSize = editor.GetPageSize(1);
            originalWidth = (float)originalSize.Width;
            originalHeight = (float)originalSize.Height;

            // Set a new custom page size (e.g., 500 × 700 points)
            editor.PageSize = new Aspose.Pdf.PageSize(500f, 700f);

            // Apply the size change to all pages (default ProcessPages = all)
            editor.ApplyChanges();

            // Save the PDF with the new page size
            editor.Save(resizedPath);
        }

        // ---------- Revert to the saved original size ----------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF that was just resized
            editor.BindPdf(resizedPath);

            // Restore the original page dimensions
            editor.PageSize = new Aspose.Pdf.PageSize(originalWidth, originalHeight);
            editor.ApplyChanges();

            // Save the reverted PDF
            editor.Save(revertedPath);
        }

        Console.WriteLine("Page size changed and then reverted successfully.");
    }
}
