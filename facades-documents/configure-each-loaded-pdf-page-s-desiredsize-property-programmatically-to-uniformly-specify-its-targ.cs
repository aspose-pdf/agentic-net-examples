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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor edits page dimensions. It implements IDisposable, so use a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Uniformly set the desired size for all pages (e.g., A4).
            editor.PageSize = PageSize.A4;

            // Apply the size changes to every page.
            editor.ApplyChanges();

            // Save the resulting PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"All pages resized and saved to '{outputPath}'.");
    }
}