using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for Rectangle and Color used by PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPath = "input.xps";   // XPS source file
        const string outputPath = "output.pdf"; // Resulting PDF with caret annotation

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade for editing PDF content
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the XPS document (Aspose.Pdf can load XPS as input)
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (position on the page)
        System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 150, 100);
        // Define the caret rectangle (actual caret bounds)
        System.Drawing.Rectangle caretRect = new System.Drawing.Rectangle(120, 520, 110, 80);
        // Symbol associated with the caret ("P" for paragraph, "None" for no symbol)
        string symbol = "None";
        // Text that appears in the annotation popup
        string contents = "Caret annotation example";
        // Color of the annotation
        System.Drawing.Color color = System.Drawing.Color.Red;

        // Create the caret annotation on page 1 (page numbers are 1‑based)
        editor.CreateCaret(
            page: 1,
            annotRect: annotRect,
            caretRect: caretRect,
            symbol: symbol,
            annotContents: contents,
            color: color);

        // Save the modified document as PDF
        editor.Save(outputPath);

        Console.WriteLine($"Caret annotation added and saved to '{outputPath}'.");
    }
}