using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade for page-level editing.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Explicitly set left horizontal alignment for all pages.
            editor.HorizontalAlignment = HorizontalAlignment.Left;

            // Apply the alignment changes.
            editor.ApplyChanges();

            // Save the result.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Horizontal alignment applied and saved to '{outputPath}'.");
    }
}