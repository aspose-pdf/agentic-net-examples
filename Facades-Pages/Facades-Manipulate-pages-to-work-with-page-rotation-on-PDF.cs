using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the page editor facade
        PdfPageEditor editor = new PdfPageEditor();

        // Bind the PDF file to the editor
        editor.BindPdf(inputPath);

        // Set rotation for all pages (must be 0, 90, 180, or 270)
        editor.Rotation = 90;

        // Apply the rotation changes to the document
        editor.ApplyChanges();

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}