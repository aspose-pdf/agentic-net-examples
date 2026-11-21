using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Reset rotation of page 6 using PdfPageEditor.
        // In recent Aspose.PDF versions the rotation is set via the
        // Rotation property together with the ProcessPages array.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new int[] { 6 }; // 1‑based page numbers
            editor.Rotation = 0; // allowed values: 0, 90, 180, 270
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 6 rotation reset. Saved to '{outputPath}'.");
    }
}
