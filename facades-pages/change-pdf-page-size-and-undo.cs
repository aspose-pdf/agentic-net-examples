using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // PageSize resides in Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string customSizePath = "custom_size.pdf";
        const string revertedPath   = "reverted.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Bind the PDF to the editor
        // ------------------------------------------------------------
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // ------------------------------------------------------------
        // 2. Capture the original page size (using the first page as reference)
        // ------------------------------------------------------------
        // GetPageSize returns a PageSize object with Width and Height in points
        PageSize originalSize = editor.GetPageSize(1);

        // ------------------------------------------------------------
        // 3. Change the page size to a custom dimension
        // ------------------------------------------------------------
        // Example custom size: 500 x 700 points
        PageSize customSize = new PageSize(500, 700);
        editor.PageSize = customSize;

        // Apply the size change to all pages (ProcessPages defaults to all pages)
        editor.ApplyChanges();

        // Save the PDF with the new page size
        editor.Save(customSizePath);
        Console.WriteLine($"PDF saved with custom size: {customSizePath}");

        // ------------------------------------------------------------
        // 4. Revert to the original page size to test undo functionality
        // ------------------------------------------------------------
        editor.PageSize = originalSize;
        editor.ApplyChanges();
        editor.Save(revertedPath);
        Console.WriteLine($"PDF reverted to original size: {revertedPath}");

        // ------------------------------------------------------------
        // 5. Clean up resources
        // ------------------------------------------------------------
        editor.Close();
    }
}