using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to work with page sizes
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Store the original size of page 8 (pages are 1‑based)
            PageSize originalSize = editor.GetPageSize(8);
            Console.WriteLine($"Original size of page 8: {originalSize.Width} x {originalSize.Height}");

            // ----- Example modification of page 8 size -----
            // Define a new size (e.g., increase width and height by 20%)
            float newWidth  = (float)(originalSize.Width  * 1.20);
            float newHeight = (float)(originalSize.Height * 1.20);

            // Restrict the operation to page 8 only
            editor.ProcessPages = new int[] { 8 };
            // Apply the new size
            editor.PageSize = new PageSize(newWidth, newHeight);
            editor.ApplyChanges();
            // ------------------------------------------------

            // Revert page 8 back to its original dimensions
            editor.ProcessPages = new int[] { 8 };
            editor.PageSize = new PageSize((float)originalSize.Width, (float)originalSize.Height);
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 8 size reverted and saved to '{outputPath}'.");
    }
}