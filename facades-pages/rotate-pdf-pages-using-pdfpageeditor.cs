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

        // Rotate all pages 90 degrees using PdfPageEditor
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);   // load the PDF
            editor.Rotation = 90;        // allowed values: 0, 90, 180, 270
            editor.ApplyChanges();       // apply the rotation
            editor.Save(outputPath);     // save the modified PDF
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}