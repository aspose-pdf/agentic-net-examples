using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reverted_page8.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (facade) to edit page size
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPath);

            // Store the original size of page 8 (pages are 1‑based)
            PageSize originalSize = editor.GetPageSize(8);

            // -----------------------------------------------------------------
            // Example modification: change page 8 size to A4 (demonstrates a change)
            // -----------------------------------------------------------------
            editor.ProcessPages = new int[] { 8 };          // target page 8
            editor.PageSize    = PageSize.A4;               // new size
            editor.ApplyChanges();                          // apply the change

            // -----------------------------------------------------------------
            // Revert page 8 back to its original dimensions
            // -----------------------------------------------------------------
            editor.ProcessPages = new int[] { 8 };          // target page 8 again
            editor.PageSize    = originalSize;              // restore original size
            editor.ApplyChanges();                          // apply the revert

            // Save the resulting PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 8 size reverted and saved to '{outputPath}'.");
    }
}