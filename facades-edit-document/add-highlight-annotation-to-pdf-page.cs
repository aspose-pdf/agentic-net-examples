using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Rectangle and Color (used by PdfContentEditor)
using Aspose.Pdf.Facades;          // Facade classes for PDF manipulation

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path and highlight color
        const string inputPdf  = "input.pdf";
        const string outputPdf = "highlighted_output.pdf";

        // Define the rectangle that covers the text to be highlighted on page 3.
        // Rectangle constructor: (x, y, width, height) – coordinates are in points.
        // Adjust these values to match the actual text location.
        Rectangle highlightRect = new Rectangle(100, 500, 200, 30); // example values

        // Highlight annotation type: 0 = Highlight (per PdfContentEditor.CreateMarkup documentation)
        const int highlightType = 0;

        // Page number is 1‑based; we need page 3.
        const int pageNumber = 3;

        // Highlight color – using System.Drawing.Color because CreateMarkup expects it.
        Color highlightColor = Color.Yellow;

        // Ensure the source file exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use the facade inside a using block for deterministic disposal.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Create the highlight markup annotation.
            // Parameters: rectangle, contents (optional comment), type, page, color.
            editor.CreateMarkup(highlightRect, "Highlighted text", highlightType, pageNumber, highlightColor);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Highlight annotation added. Saved to '{outputPdf}'.");
    }
}