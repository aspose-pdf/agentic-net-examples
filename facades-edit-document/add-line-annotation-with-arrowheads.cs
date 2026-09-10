using System;
using System.Drawing;                     // System.Drawing is required for Rectangle and Color used by PdfContentEditor
using System.IO;                         // For File.Exists
using Aspose.Pdf;
using Aspose.Pdf.Facades;                // PdfContentEditor facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Load the PDF using the facade, then add a line annotation with arrowheads at both ends.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (position on the page).
            // The rectangle can be zero‑size because the line coordinates define the visual.
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(0, 0, 0, 0);

            // Annotation contents (optional tooltip text).
            string contents = "Line with arrows at both ends";

            // Line start and end coordinates (in points).
            float x1 = 100f;   // start X
            float y1 = 500f;   // start Y
            float x2 = 300f;   // end X
            float y2 = 500f;   // end Y

            // Page number (1‑based indexing).
            int pageNumber = 1;

            // Border width (1 point).
            int borderWidth = 1;

            // Line color – use System.Drawing.Color as required by the CreateLine overload.
            System.Drawing.Color lineColor = System.Drawing.Color.Red;

            // Border style ("S" = solid). Not dashed, so dashArray can be null.
            string borderStyle = "S";

            // Arrowhead styles: first element for start, second for end.
            // Use "ClosedArrow" for both ends.
            string[] lineEndings = new string[] { "ClosedArrow", "ClosedArrow" };

            // Create the line annotation.
            editor.CreateLine(
                annotRect,
                contents,
                x1, y1, x2, y2,
                pageNumber,
                borderWidth,
                lineColor,
                borderStyle,
                null,          // dashArray not needed for solid line
                lineEndings    // arrowheads at start and end
            );

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Line annotation with arrowheads saved to '{outputPath}'.");
    }
}
