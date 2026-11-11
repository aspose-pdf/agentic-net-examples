using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated.pdf";
        const int    pageNumber = 1;   // 1‑based page index to rotate
        const int    rotation   = 90;  // allowed values: 0, 90, 180, 270

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor does NOT implement IDisposable, so we instantiate it directly.
        var editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Specify which page(s) the editor should affect.
        editor.ProcessPages = new[] { pageNumber };
        // Set the rotation for the selected page(s). Accepted values are 0, 90, 180, 270.
        editor.Rotation = rotation;

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Page {pageNumber} rotated to {rotation}° and saved as '{outputPath}'.");
    }
}
