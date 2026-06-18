using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color (PdfContentEditor API)
using Aspose.Pdf.Facades;          // Facade API for annotation creation

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

        // PdfContentEditor implements IDisposable – use a using block for deterministic disposal
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Annotation rectangle (position on the page). Size can be zero; the line coordinates define the visual.
            Rectangle annotRect = new Rectangle(0, 0, 200, 200);

            // Define start and end points of the line
            float x1 = 50f, y1 = 50f;   // start point
            float x2 = 150f, y2 = 150f; // end point

            // Border width (in points)
            int borderWidth = 1;

            // Line color (System.Drawing.Color is required by the CreateLine method)
            Color lineColor = Color.Red;

            // Border style: "S" = solid, "D" = dashed, etc.
            string borderStyle = "S";

            // No dash pattern needed for a solid line
            int[] dashArray = null;

            // Arrowheads at both ends: OpenArrow for start and end
            string[] leArray = new string[] { "OpenArrow", "OpenArrow" };

            // Create the line annotation on page 1 (pages are 1‑based)
            editor.CreateLine(
                annotRect,
                "Line with arrowheads", // annotation contents (optional)
                x1, y1, x2, y2,
                1,                     // page number
                borderWidth,
                lineColor,
                borderStyle,
                dashArray,
                leArray);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Line annotation with arrowheads saved to '{outputPath}'.");
    }
}