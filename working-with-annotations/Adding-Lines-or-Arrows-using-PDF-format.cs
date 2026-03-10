using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (a Facades class) to add a line/arrow annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Annotation rectangle – not used for line geometry, can be zero-sized
            Rectangle annotRect = new Rectangle(0, 0, 0, 0);

            // Define line start and end coordinates (in points)
            float x1 = 100f; // start X
            float y1 = 500f; // start Y
            float x2 = 300f; // end X
            float y2 = 500f; // end Y

            // Page number is 1‑based
            int pageNumber = 1;

            // Border width (in points)
            int borderWidth = 2;

            // Line color
            Color lineColor = Color.Blue;

            // Border style: "S" = solid, "D" = dashed, etc.
            string borderStyle = "S";

            // Dash pattern – null for solid line
            int[] dashArray = null;

            // Arrowhead styles for start and end points.
            // Valid values: "Square", "Circle", "Diamond", "OpenArrow",
            // "ClosedArrow", "None", "Butt", "ROpenArrow", "RClosedArrow", "Slash"
            string[] lineEndingArray = new string[] { "OpenArrow", "None" };

            // Create the line annotation with the specified parameters
            editor.CreateLine(
                annotRect,
                "Sample line with arrow", // annotation contents (tooltip)
                x1, y1, x2, y2,
                pageNumber,
                borderWidth,
                lineColor,
                borderStyle,
                dashArray,
                lineEndingArray);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPdf}'.");
    }
}