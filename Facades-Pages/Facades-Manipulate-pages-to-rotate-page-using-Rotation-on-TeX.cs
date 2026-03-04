using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade that allows page‑level operations such as rotation.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Set the rotation angle (must be 0, 90, 180 or 270).
            // This value will be applied to all pages because ProcessPages defaults to all pages.
            editor.Rotation = 90;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF to the desired output file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}