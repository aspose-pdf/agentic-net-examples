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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to modify page layout
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Apply changes only to page 2
            editor.ProcessPages = new int[] { 2 };

            // Center the original content horizontally on the result page
            // Use the non‑obsolete Aspose.Pdf.HorizontalAlignment enum
            editor.HorizontalAlignment = HorizontalAlignment.Center;

            // Apply the configured changes
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 2 content centered horizontally and saved to '{outputPath}'.");
    }
}
