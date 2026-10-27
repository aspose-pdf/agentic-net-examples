using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Align the content of page 2 to the left using PdfPageEditor
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);
        editor.ProcessPages = new int[] { 2 }; // 1‑based page index
        editor.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Left;
        editor.Save(outputPath);

        Console.WriteLine($"Page 2 content left‑aligned and saved to '{outputPath}'.");
    }
}
