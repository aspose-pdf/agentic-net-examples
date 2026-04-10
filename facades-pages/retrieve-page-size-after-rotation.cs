using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfPageEditor
using Aspose.Pdf;          // PageSize

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the editor and rotate the first page.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Set rotation (applies to all pages unless ProcessPages is set).
            editor.Rotation = 90;
            editor.ApplyChanges();

            // Retrieve the page size after rotation.
            PageSize size = editor.GetPageSize(1);
            Console.WriteLine($"Page 1 size after rotation: {size.Width} x {size.Height}");

            // Verify the rotation value.
            int rotation = editor.GetPageRotation(1);
            Console.WriteLine($"Page 1 rotation: {rotation} degrees");
        }
    }
}