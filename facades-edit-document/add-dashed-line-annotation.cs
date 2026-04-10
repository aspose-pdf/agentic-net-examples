using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "diagram.pdf";      // source PDF containing the workflow diagram
        const string outputPdf = "diagram_with_line.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfContentEditor is a disposable facade – use a using block for deterministic cleanup
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (position and size) – fully qualified to avoid ambiguity
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 2);

            // Custom dash pattern: 4 points dash, 2 points gap
            int[] dashPattern = new int[] { 4, 2 };

            // No special line endings (both ends are plain)
            string[] lineEndings = new string[] { "None", "None" };

            // Create the line annotation on page 1
            // Parameters:
            // rect, contents, x1, y1, x2, y2, page, border width, color, border style, dash array, line ending array
            editor.CreateLine(
                annotRect,
                "Workflow Step Highlight",
                100f, 500f,   // start point (x1, y1)
                300f, 500f,   // end point   (x2, y2)
                1,            // page number (1‑based)
                1,            // border width in points
                System.Drawing.Color.Red, // line color
                "D",          // border style "D" = Dashed
                dashPattern,  // custom dash pattern
                lineEndings   // line ending styles
            );

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPdf}'.");
    }
}