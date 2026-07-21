using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade classes including PdfPageEditor

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

        // PdfPageEditor allows editing page layout such as vertical alignment.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Define which pages to modify. Example: pages 1, 2 and 3.
            editor.ProcessPages = new int[] { 1, 2, 3 };

            // Align the original content to the top of each result page.
            // Use the non‑obsolete enum from the Facades namespace.
            editor.VerticalAlignment = Aspose.Pdf.Facades.VerticalAlignmentType.Top;

            // Apply the configured changes.
            editor.ApplyChanges();

            // Save the edited document.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Vertical alignment applied and saved to '{outputPath}'.");
    }
}
