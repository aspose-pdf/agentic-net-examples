using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Rectangle and System.Drawing.Color (used by PdfContentEditor)
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_line.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the Facades editor and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Annotation rectangle – size is irrelevant for line geometry, can be zero
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(0, 0, 0, 0);

            // Define arrowhead styles for start and end points
            string[] leArray = new string[] { "OpenArrow", "OpenArrow" };

            // Create a line annotation on page 1 from (100,500) to (300,500)
            editor.CreateLine(
                annotRect,                 // annotation rectangle (System.Drawing.Rectangle)
                "Line with arrows",        // contents (tooltip)
                100, 500,                  // x1, y1 (start point)
                300, 500,                  // x2, y2 (end point)
                1,                         // page number (1‑based)
                1,                         // border width
                System.Drawing.Color.Blue, // line color (System.Drawing.Color)
                "S",                       // border style: solid
                null,                      // dash array (null for solid line)
                leArray);                  // line ending array (arrowheads at both ends)

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Line annotation with arrowheads saved to '{outputPath}'.");
    }
}
