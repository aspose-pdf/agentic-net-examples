using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "custom_page.pdf";
        const double customWidth = 500;   // width in points (1 inch = 72 points)
        const double customHeight = 700;  // height in points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor from Aspose.Pdf.Facades to modify page size
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Set a non‑standard page size for all pages
            editor.PageSize = new PageSize((float)customWidth, (float)customHeight);

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the result
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom page size to '{outputPath}'.");
    }
}