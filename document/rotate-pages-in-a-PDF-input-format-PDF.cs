using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (a Facades class) to rotate pages.
        // The class implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file to the editor.
            editor.BindPdf(inputPath);

            // Set the rotation angle (must be 0, 90, 180 or 270 degrees).
            // Here we rotate all pages by 90 degrees clockwise.
            editor.Rotation = 90;

            // Optional: specify which pages to rotate.
            // If omitted, all pages are processed.
            // editor.ProcessPages = new int[] { 1, 2, 3 }; // example for specific pages

            // Apply the rotation changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF to the desired output file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}