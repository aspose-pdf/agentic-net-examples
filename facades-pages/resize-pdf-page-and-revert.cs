using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfPageEditor

class Program
{
    static void Main()
    {
        const string inputPath    = "input.pdf";
        const string resizedPath  = "resized.pdf";
        const string revertedPath = "reverted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Change page size to custom dimensions, then revert to original size
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Store original size of the first page (pages are 1‑based)
            PageSize originalSize = editor.GetPageSize(1);
            float originalWidth  = originalSize.Width;
            float originalHeight = originalSize.Height;

            // Set a custom page size (example: 500 x 700 points)
            editor.PageSize = new PageSize(500f, 700f);
            editor.ApplyChanges();                 // Apply the size change
            editor.Save(resizedPath);               // Save the resized PDF
            Console.WriteLine($"Resized PDF saved to '{resizedPath}'");

            // Revert to the original page size
            editor.PageSize = new PageSize(originalWidth, originalHeight);
            editor.ApplyChanges();                 // Apply the revert
            editor.Save(revertedPath);              // Save the reverted PDF
            Console.WriteLine($"Reverted PDF saved to '{revertedPath}'");
        }
    }
}
