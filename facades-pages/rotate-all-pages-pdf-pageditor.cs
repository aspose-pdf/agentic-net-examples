using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfPageEditor lives in the Facades namespace

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

        // Load the PDF using PdfPageEditor (no native process is spawned)
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Rotate every page 90 degrees clockwise.
        // PdfPageEditor uses an integer rotation (0, 90, 180, 270).
        editor.Rotation = 90;

        // Save the edited PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
