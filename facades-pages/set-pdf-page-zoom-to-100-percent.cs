using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a disposable facade; use a using block for deterministic cleanup
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Set the zoom coefficient to 1.0 (100 % scaling) for all pages
            editor.Zoom = 1.0f;

            // Apply the changes made to the document
            editor.ApplyChanges();

            // Save the updated PDF to the desired output path
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom set to 1.0 and saved to '{outputPath}'.");
    }
}