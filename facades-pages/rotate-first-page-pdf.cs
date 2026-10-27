using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "edited.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor does NOT implement IDisposable, so no using block is needed
        PdfPageEditor pageEditor = new PdfPageEditor();

        // Load (bind) the PDF file for editing
        pageEditor.BindPdf(inputPath);

        // Prepare the editor to work on the first page (1‑based index)
        pageEditor.ProcessPages = new int[] { 1 };
        // Rotation is specified in degrees: 0, 90, 180, 270
        pageEditor.Rotation = 90; // rotate first page 90° clockwise

        // Save the edited PDF to a new file
        pageEditor.Save(outputPath);

        Console.WriteLine($"PDF loaded and edited successfully. Saved to '{outputPath}'.");
    }
}
