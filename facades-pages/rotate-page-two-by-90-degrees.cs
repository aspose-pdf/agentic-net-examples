using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_page2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Rotate page 2 by 90 degrees using PdfPageEditor
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);               // Load the PDF
            editor.PageRotations[2] = 90;            // Page numbers are 1‑based
            editor.ApplyChanges();                   // Apply the rotation
            editor.Save(outputPath);                 // Save the result
        }

        Console.WriteLine($"Page 2 rotated and saved to '{outputPath}'.");
    }
}