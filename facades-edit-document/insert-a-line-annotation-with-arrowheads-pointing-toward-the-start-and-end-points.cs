using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color (PdfContentEditor API)
using Aspose.Pdf.Facades;          // Facade for annotation operations

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the facade, load the PDF, add the line annotation, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Annotation rectangle – can be zero‑size because the line coordinates define the geometry.
            Rectangle annotRect = new Rectangle(0, 0, 0, 0);

            // Arrowhead styles for the start and end points.
            // Valid values: "Square", "Circle", "Diamond", "OpenArrow", "ClosedArrow",
            // "None", "Butt", "ROpenArrow", "RClosedArrow", "Slash".
            string[] lineEndings = new string[] { "ClosedArrow", "ClosedArrow" };

            // Create a line annotation on page 1 from (100,500) to (300,500) with red color.
            editor.CreateLine(
                annotRect,               // Annotation rectangle
                "Line with arrows",      // Contents (optional tooltip)
                100f, 500f,              // x1, y1 (start point)
                300f, 500f,              // x2, y2 (end point)
                1,                       // Page number (1‑based)
                1,                       // Border width (points)
                Color.Red,               // Line color (System.Drawing.Color)
                "S",                     // Border style: "S" = solid
                null,                    // Dash array (null for solid line)
                lineEndings              // Arrowhead styles for start and end
            );

            // Save the modified document.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Line annotation with arrowheads saved to '{outputPath}'.");
    }
}