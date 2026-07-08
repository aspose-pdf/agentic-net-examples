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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the source PDF to the editor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve the original size of the first page (points)
            PageSize originalSize = editor.GetPageSize(1);
            Console.WriteLine($"Original size: {originalSize.Width} x {originalSize.Height} points");

            // Set the output page size to landscape by swapping width and height
            editor.PageSize = new PageSize(originalSize.Height, originalSize.Width);

            // Rotate the page 90° so the content remains upright
            editor.Rotation = 90;

            // Apply the modifications to the document
            editor.ApplyChanges();

            // Verify the new page dimensions and rotation
            PageSize newSize = editor.GetPageSize(1);
            int newRotation = editor.GetPageRotation(1);
            Console.WriteLine($"New size: {newSize.Width} x {newSize.Height} points");
            Console.WriteLine($"Rotation after edit: {newRotation} degrees");

            // Save the edited PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Landscape PDF saved to '{outputPath}'.");
    }
}