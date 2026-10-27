using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for VerticalAlignment enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        // 1‑based page numbers that should be aligned to the top
        int[] pagesToAlign = { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Use PdfPageEditor within a using block to ensure resources are released
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the source PDF
                editor.BindPdf(inputPath);

                // Set vertical alignment to the top of the page (new API)
                editor.VerticalAlignmentType = VerticalAlignment.Top;

                // Specify the pages that the alignment should be applied to
                editor.ProcessPages = pagesToAlign;

                // Save the resulting PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Vertical alignment applied. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
