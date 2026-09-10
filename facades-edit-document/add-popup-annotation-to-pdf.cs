using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so use a using block for deterministic cleanup.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file to the editor.
            editor.BindPdf(inputPath);

            // Create a popup annotation.
            // Rectangle(x, y, width, height) – coordinates are in points.
            // Contents: text shown inside the popup.
            // open: false – the popup is not displayed initially; it appears when the user hovers/clicks.
            // page: 1‑based page number where the annotation will be placed.
            editor.CreatePopup(
                new System.Drawing.Rectangle(100, 500, 200, 100),
                "This is a note that appears when you hover over the annotation.",
                false,
                1);

            // Save the modified PDF.
            editor.Save(outputPath);
            // editor.Close() is called automatically by Dispose().
        }

        Console.WriteLine($"Popup annotation added. Output saved to '{outputPath}'.");
    }
}