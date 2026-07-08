using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Rectangle and System.Drawing.Color (PdfContentEditor API)
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_line.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the Facades editor and bind the PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Annotation rectangle – zero size is acceptable for line annotations
        // Use System.Drawing.Rectangle as required by PdfContentEditor.CreateLine overload
        System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(0, 0, 0, 0);

        // Define start and end coordinates of the line
        float x1 = 100f, y1 = 200f;   // start point
        float x2 = 400f, y2 = 200f;   // end point

        // Arrowhead styles for start and end points (both pointing inward)
        string[] leArray = new string[] { "OpenArrow", "OpenArrow" };

        // Create the line annotation on page 1
        editor.CreateLine(
            annotRect,                 // annotation rectangle (System.Drawing.Rectangle)
            "Line with arrows",      // contents (tooltip)
            x1, y1, x2, y2,           // line coordinates
            1,                         // page number (1‑based)
            1,                         // border width
            System.Drawing.Color.Red, // line color (System.Drawing.Color)
            "S",                      // solid border style
            null,                      // dash array (null for solid line)
            leArray);                  // line ending styles (arrowheads)

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Line annotation saved to '{outputPath}'.");
    }
}
