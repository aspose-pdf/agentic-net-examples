using System;
using System.Drawing;               // System.Drawing.Color is required by PdfContentEditor APIs
using Aspose.Pdf.Facades;          // Facade classes for annotation handling

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the result PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the facade, bind the PDF and add a red highlight annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Define the rectangle (in points) where the highlight will appear.
            // Rectangle(x, y, width, height) – origin is at the bottom‑left of the page.
            // Adjust these values to match the area you want to emphasize.
            Rectangle highlightRect = new Rectangle(100, 500, 200, 20);

            // Create a highlight markup (type = 0) on page 1 with bright red color.
            // The color parameter uses System.Drawing.Color.
            editor.CreateMarkup(
                rect:       highlightRect,
                contents:   "Important text",
                type:       0,                     // 0 = Highlight
                page:       1,                     // 1‑based page index
                clr:        Color.FromArgb(255, 0, 0) // RGB(255,0,0) – bright red
            );

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Red highlight annotation saved to '{outputPath}'.");
    }
}