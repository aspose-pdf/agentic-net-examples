using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into PdfPageEditor, set rotation, apply changes, and save.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);   // load PDF
            editor.Rotation = 90;        // allowed values: 0, 90, 180, 270
            editor.ApplyChanges();       // apply rotation to pages
            editor.Save(outputPath);     // save the rotated PDF
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}