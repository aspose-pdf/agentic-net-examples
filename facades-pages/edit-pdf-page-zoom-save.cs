using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor does not implement IDisposable, so we instantiate it directly.
        PdfPageEditor pageEditor = new PdfPageEditor();

        // Load the source PDF.
        pageEditor.BindPdf(inputPath);

        // Specify which pages to process (1‑based indexing). Here we rotate only the first page.
        pageEditor.ProcessPages = new int[] { 1 };

        // Set the rotation in degrees (valid values: 0, 90, 180, 270).
        pageEditor.Rotation = 90;

        // Save the edited PDF to a new file.
        pageEditor.Save(outputPath);

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
