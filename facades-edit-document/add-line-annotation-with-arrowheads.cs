using System;
using System.IO;
using System.Drawing;               // needed for Rectangle and Color
using Aspose.Pdf.Facades;          // PdfContentEditor facade

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

        // Bind the PDF and add a line annotation with arrowheads at both ends
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Annotation rectangle (zero‑size is acceptable for line annotations)
            Rectangle annotRect = new Rectangle(0, 0, 0, 0);

            // Arrowhead styles for start and end points
            string[] leArray = new string[] { "OpenArrow", "OpenArrow" };

            // Create the line annotation on page 1
            editor.CreateLine(
                annotRect,               // annotation rectangle
                "Line with arrows",      // contents (tooltip)
                100f, 500f,              // start point (x1, y1)
                300f, 500f,              // end point   (x2, y2)
                1,                       // page number (1‑based)
                1,                       // border width
                Color.Red,               // line color (System.Drawing.Color)
                "S",                     // border style: solid
                null,                    // dash array (null for solid)
                leArray);                // line ending styles (arrowheads)

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Line annotation with arrowheads saved to '{outputPath}'.");
    }
}