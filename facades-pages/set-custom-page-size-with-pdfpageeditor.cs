using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "customsize.pdf";

        // Custom page dimensions in points (1 inch = 72 points)
        const double customWidth  = 500; // e.g., 500 points
        const double customHeight = 800; // e.g., 800 points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable, so wrap in using
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Set the new page size for all pages (or set ProcessPages to target specific pages)
            editor.PageSize = new PageSize((float)customWidth, (float)customHeight);

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom page size to '{outputPath}'.");
    }
}