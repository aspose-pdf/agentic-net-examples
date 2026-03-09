using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_curve.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor and bind it to the loaded document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Define the curve geometry (example: a simple polyline)
                LineInfo lineInfo = new LineInfo
                {
                    // VerticeCoordinate defines the points of the curve: x1, y1, x2, y2, ...
                    VerticeCoordinate = new float[] { 100f, 500f, 200f, 600f, 300f, 500f },
                    Visibility = true
                };

                // Annotation rectangle (position and size) – use System.Drawing.Rectangle to avoid ambiguity
                System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(0, 0, 0, 0);

                // Add the curve annotation on page 1
                editor.DrawCurve(lineInfo, 1, annotRect, "Sample curve annotation");

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Curve inserted and saved to '{outputPath}'.");
    }
}