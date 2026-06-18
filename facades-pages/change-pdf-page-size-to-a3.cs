using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_A3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade that allows page size modification.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Set the output page size to A3 (420 mm × 297 mm).
            editor.PageSize = PageSize.A3;

            // Apply the changes to all pages.
            editor.ApplyChanges();

            // Save the resulting PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with A3 page size to '{outputPath}'.");
    }
}