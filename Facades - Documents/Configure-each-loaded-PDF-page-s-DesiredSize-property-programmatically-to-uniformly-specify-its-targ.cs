using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired page dimensions (points). Example: A4 size = 595 x 842 points.
        double targetWidth  = 595; // width in points
        double targetHeight = 842; // height in points

        // PdfPageEditor edits page size, rotation, margins, etc.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF to be edited.
            editor.BindPdf(inputPath);

            // Uniformly set the target size for all pages.
            editor.PageSize = new PageSize((float)targetWidth, (float)targetHeight);

            // Apply the changes to the bound document.
            editor.ApplyChanges();

            // Save the result.
            editor.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to {targetWidth}x{targetHeight} points and saved as '{outputPath}'.");
    }
}