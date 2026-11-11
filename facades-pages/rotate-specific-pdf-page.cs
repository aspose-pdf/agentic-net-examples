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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF using PdfPageEditor (does not implement IDisposable)
        var editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Specify the page(s) to affect – PdfPageEditor uses 1‑based indexing
        editor.ProcessPages = new int[] { 4 };
        // Set the rotation angle (allowed values: 0, 90, 180, 270)
        editor.Rotation = 180;

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Page 4 rotated and saved to '{outputPath}'.");
    }
}
