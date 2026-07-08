using System;
using System.IO;
using Aspose.Pdf;               // For PageSize enum
using Aspose.Pdf.Facades;      // For PdfPageEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable, so wrap it in a using block
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file to the editor
            editor.BindPdf(inputPath);

            // -----------------------------------------------------------------
            // Chain multiple page modifications:
            // 1. Rotate all pages by 90 degrees
            // 2. Set zoom factor to 75% (0.75)
            // 3. Change the output page size to A4
            // 4. Move the origin of the page content (optional example)
            // -----------------------------------------------------------------
            editor.Rotation = 90;               // Rotation must be 0, 90, 180 or 270
            editor.Zoom = 0.75f;                // 1.0 = 100%
            editor.PageSize = PageSize.A4;      // Use Aspose.Pdf.PageSize enum
            editor.MovePosition(50f, 100f);    // Shift origin right 50pt, up 100pt

            // Apply the queued changes to the document
            editor.ApplyChanges();

            // Save the modified PDF to the specified output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}