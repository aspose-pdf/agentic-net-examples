using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "portrait.pdf";
        const string outputPath = "landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the source PDF to the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve the original size of the first page (pages are 1‑based)
            PageSize originalSize = editor.GetPageSize(1);
            float originalWidth = originalSize.Width;
            float originalHeight = originalSize.Height;

            // Set the output page size to landscape by swapping width and height
            editor.PageSize = new PageSize(originalHeight, originalWidth);

            // Rotate the page content 90 degrees to match the new orientation
            editor.Rotation = 90;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Verify the new dimensions and rotation
            PageSize newSize = editor.GetPageSize(1);
            int newRotation = editor.GetPageRotation(1);
            Console.WriteLine($"New page size – Width: {newSize.Width}, Height: {newSize.Height}");
            Console.WriteLine($"Page rotation after change: {newRotation} degrees");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Landscape PDF saved to '{outputPath}'.");
    }
}