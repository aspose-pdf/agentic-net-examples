using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "portrait.pdf";
        const string outputPath = "landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Work on the first page (indexing is 1‑based)
            editor.ProcessPages = new int[] { 1 };

            // Retrieve the original page size
            PageSize originalSize = editor.GetPageSize(1);
            Console.WriteLine($"Original size: {originalSize.Width} x {originalSize.Height}");

            // Swap width and height to obtain a landscape page size
            editor.PageSize = new PageSize(originalSize.Height, originalSize.Width);

            // Rotate the page 90° so the content stays upright
            editor.Rotation = 90;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Verify the new dimensions and rotation
            PageSize newSize = editor.GetPageSize(1);
            int rotation = editor.GetPageRotation(1);
            Console.WriteLine($"New size: {newSize.Width} x {newSize.Height}");
            Console.WriteLine($"Rotation: {rotation} degrees");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Landscape PDF saved to '{outputPath}'.");
    }
}