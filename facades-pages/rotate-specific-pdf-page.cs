using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_page4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor does not implement IDisposable, so we instantiate it directly.
        var pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(inputPath);

        // Target page 4 (1‑based indexing) and set its rotation to 180°.
        pageEditor.ProcessPages = new int[] { 4 };
        pageEditor.Rotation = 180; // allowed values: 0, 90, 180, 270

        // Save the modified PDF.
        pageEditor.Save(outputPath);

        Console.WriteLine($"Page 4 rotated and saved to '{outputPath}'.");
    }
}
