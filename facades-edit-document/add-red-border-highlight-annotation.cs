using System;
using System.Drawing; // for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "highlighted_output.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // System.Drawing.Rectangle (x, y, width, height) – coordinates are in points.
            System.Drawing.Rectangle highlightRect = new System.Drawing.Rectangle(100, 500, 200, 20);

            // System.Drawing.Color for bright‑red border (RGB 255,0,0).
            System.Drawing.Color redColor = System.Drawing.Color.FromArgb(255, 0, 0);

            // Create a highlight annotation (type 0 = Highlight) on page 1.
            editor.CreateMarkup(
                rect:    highlightRect,
                contents: "Important section",
                type:     0,          // 0 = Highlight
                page:     1,          // first page (1‑based)
                clr:      redColor    // bright red border/color
            );

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Highlight annotation with red border saved to '{outputPdf}'.");
    }
}
