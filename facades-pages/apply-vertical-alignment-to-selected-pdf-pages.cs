using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        // Pages to which vertical alignment will be applied (1‑based indexing)
        int[] selectedPages = new int[] { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable in recent versions, so use a using block
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Set vertical alignment to Top for consistent top positioning
            editor.VerticalAlignmentType = Aspose.Pdf.VerticalAlignment.Top;

            // Specify the pages that should receive the alignment
            editor.ProcessPages = selectedPages;

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Aligned PDF saved to '{outputPath}'.");
    }
}
