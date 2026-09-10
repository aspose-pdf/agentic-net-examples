using System;
using System.Drawing; // Required for System.Drawing.Color used by PdfContentEditor
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output_highlighted.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the facade that works with annotations
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document
        editor.BindPdf(inputPdf);

        // Define the rectangle (in points) where the highlight will appear
        // (left, top, width, height) – System.Drawing.Rectangle uses X,Y as top‑left corner
        Rectangle highlightRect = new Rectangle(100, 500, 200, 50);

        // Create a Highlight markup annotation (type = 0) on page 1 with bright red color
        // The color parameter sets the annotation's visual color (used for the border/highlight)
        editor.CreateMarkup(
            rect:        highlightRect,
            contents:    "Important section",
            type:        0,                 // 0 = Highlight
            page:        1,                 // 1‑based page index
            clr:         Color.FromArgb(255, 0, 0) // Bright red (RGB 255,0,0)
        );

        // Save the modified PDF
        editor.Save(outputPdf);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Highlight annotation with red border saved to '{outputPdf}'.");
    }
}