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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve the original size of the first page (pages are 1‑based)
            PageSize originalSize = editor.GetPageSize(1);
            Console.WriteLine($"Original size – Width: {originalSize.Width}, Height: {originalSize.Height}");

            // Create a landscape size by swapping width and height
            PageSize landscapeSize = new PageSize(originalSize.Height, originalSize.Width);
            editor.PageSize = landscapeSize;

            // Rotate the page content 90° so it appears correctly in landscape orientation
            editor.Rotation = 90;

            // Apply the modifications to the document
            editor.ApplyChanges();

            // Verify the new page dimensions and orientation
            PageSize newSize = editor.GetPageSize(1);
            Console.WriteLine($"New size – Width: {newSize.Width}, Height: {newSize.Height}");
            Console.WriteLine($"IsLandscape: {newSize.IsLandscape}");

            // Save the edited PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Landscape PDF saved to '{outputPath}'.");
    }
}