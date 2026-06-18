using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to modify page size/orientation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Set the output page size to A4 landscape.
            // A4 portrait dimensions are 297 (width) x 210 (height) points.
            // Landscape orientation swaps width and height.
            PageSize landscapeA4 = new PageSize(PageSize.A4.Height, PageSize.A4.Width);
            editor.PageSize = landscapeA4;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Landscape PDF saved to '{outputPath}'.");
    }
}