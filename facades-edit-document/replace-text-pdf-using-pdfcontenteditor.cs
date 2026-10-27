using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Example edit: add a simple text annotation on the first page.
            // PdfContentEditor.CreateText expects a System.Drawing.Rectangle.
            // Rectangle constructor: (x, y, width, height).
            // Convert the Aspose rectangle (left, bottom, right, top) to System.Drawing.Rectangle.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                x: 100,               // left
                y: 500,               // bottom (Y coordinate in PDF space)
                width: 300 - 100,     // right - left
                height: 550 - 500);   // top - bottom

            editor.CreateText(
                rect,
                "Edited with PdfContentEditor",
                "Arial",      // font name
                true,          // embed the font
                "Blue",       // text color (named color or hex string)
                0);            // open flag (0 = false, 1 = true)

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
