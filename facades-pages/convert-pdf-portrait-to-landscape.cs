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

        // Initialize the PdfPageEditor facade and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve the original size of the first page (portrait)
            var originalSize = editor.GetPageSize(1);
            Console.WriteLine($"Original size – Width: {originalSize.Width}, Height: {originalSize.Height}");

            // Set the output page size to landscape by swapping width and height
            editor.PageSize = new PageSize(originalSize.Height, originalSize.Width);

            // Rotate the page content 90 degrees to match the new orientation
            editor.Rotation = 90;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Verify the new page dimensions after rotation
            var newSize = editor.GetPageSize(1);
            Console.WriteLine($"New size – Width: {newSize.Width}, Height: {newSize.Height}");

            // Save the modified PDF (output will be in landscape orientation)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Landscape PDF saved to '{outputPath}'.");
    }
}