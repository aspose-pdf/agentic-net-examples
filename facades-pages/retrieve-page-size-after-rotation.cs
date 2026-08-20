using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Bind the PDF to the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Rotate the first page by 90 degrees (optional – demonstrates the change)
            editor.Rotation = 90;          // sets rotation for all pages; can also use PageRotations dictionary
            editor.ApplyChanges();         // apply the rotation to the document

            // Get the rotation of page 1 after applying changes
            int rotation = editor.GetPageRotation(1);
            Console.WriteLine($"Rotation of page 1: {rotation} degrees");

            // Get the page size of page 1 after rotation
            PageSize size = editor.GetPageSize(1);
            Console.WriteLine($"Page 1 size after rotation: {size.Width} x {size.Height}");

            // Save the modified PDF (optional)
            const string outputPath = "rotated.pdf";
            editor.Save(outputPath);
        }
    }
}