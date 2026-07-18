using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the editor and work within a using block for proper disposal
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve original page size (page numbers are 1‑based)
            PageSize originalSize = editor.GetPageSize(1);
            Console.WriteLine($"Original size: {originalSize.Width} x {originalSize.Height}");

            // Rotate page 1 by 90 degrees
            editor.Rotation = 90;                 // set rotation angle (0, 90, 180, 270)
            editor.ProcessPages = new int[] { 1 }; // apply rotation only to page 1
            editor.ApplyChanges();                // commit the rotation

            // Retrieve page size after rotation
            PageSize rotatedSize = editor.GetPageSize(1);
            Console.WriteLine($"Rotated size: {rotatedSize.Width} x {rotatedSize.Height}");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine("Operation completed.");
    }
}