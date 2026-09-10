using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // PageSize enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "brochure_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Rotate all pages to landscape and fit standard A4 brochure size
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Rotate pages 90 degrees (portrait → landscape)
            editor.Rotation = 90;

            // Set the target page size to A4 (landscape orientation)
            editor.PageSize = PageSize.A4;

            // Apply the modifications
            editor.ApplyChanges();

            // Save the resulting PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Brochure PDF saved to '{outputPath}'.");
    }
}