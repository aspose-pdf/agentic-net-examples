using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath       = "input.pdf";
        const string resizedPath     = "resized.pdf";
        const string revertedPath    = "reverted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to modify page size.
        // The facade does not implement IDisposable, so we do not wrap it in a using block.
        PdfPageEditor editor = new PdfPageEditor();

        // Bind the source PDF.
        editor.BindPdf(inputPath);

        // Retrieve the original size of the first page (pages are 1‑based).
        PageSize originalSize = editor.GetPageSize(1);
        Console.WriteLine($"Original size: {originalSize.Width} x {originalSize.Height}");

        // -----------------------------------------------------------------
        // Step 1: Change to a custom size.
        // -----------------------------------------------------------------
        // Define a custom page size (e.g., 500 x 800 points).
        editor.PageSize = new PageSize(500f, 800f);
        editor.ApplyChanges();                     // Apply the size change.
        editor.Save(resizedPath);                  // Save the modified document.
        Console.WriteLine($"Resized PDF saved to '{resizedPath}'.");

        // -----------------------------------------------------------------
        // Step 2: Revert to the saved original size.
        // -----------------------------------------------------------------
        // Restore the original page size captured earlier.
        editor.PageSize = originalSize;
        editor.ApplyChanges();                     // Apply the revert.
        editor.Save(revertedPath);                 // Save the reverted document.
        Console.WriteLine($"Reverted PDF saved to '{revertedPath}'.");
    }
}