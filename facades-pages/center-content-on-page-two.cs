using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_centered_page2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable, so we use a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Apply changes only to page 2.
            editor.ProcessPages = new int[] { 2 };

            // Align the original content horizontally to the centre of the page.
            // Use the non‑obsolete Aspose.Pdf.HorizontalAlignment enum.
            editor.HorizontalAlignment = HorizontalAlignment.Center;

            // Apply the modifications and save the result.
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Centered content saved to '{outputPath}'.");
    }
}
