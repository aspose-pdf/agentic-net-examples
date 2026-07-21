using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "diagram.pdf";
        const string outputPath = "diagram_annotated.pdf";

        // Ensure the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PDF content editor (Facades API)
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (position on the page)
        // Width and height are not used for line annotations, but a non‑zero rectangle is required.
        Rectangle annotRect = new Rectangle(100, 200, 0, 0);

        // Create a line annotation:
        // - contents: tooltip text
        // - start point (x1, y1) and end point (x2, y2)
        // - page number (1‑based)
        // - border width (1 point)
        // - line color (Blue)
        // - border style "D" for dashed
        // - dash pattern: 3 units on, 2 units off
        // - line ending styles: none for both start and end
        editor.CreateLine(
            annotRect,
            "Workflow step",
            100f, 200f,   // start coordinates
            300f, 400f,   // end coordinates
            1,            // page number (first page)
            1,            // border width
            Color.Blue,   // line color
            "D",          // dashed border style
            new int[] { 3, 2 },               // custom dash pattern
            new string[] { "None", "None" }   // no arrowheads
        );

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}