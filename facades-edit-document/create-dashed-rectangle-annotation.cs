using System;
using System.IO;
using System.Drawing;               // Rectangle and Color structs
using Aspose.Pdf.Facades;          // PdfContentEditor facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_dashed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the facade editor
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define rectangle coordinates (lower‑left and upper‑right)
        // Rectangle(x, y, width, height) – x and y are the lower‑left corner
        Rectangle rect = new Rectangle(100, 500, 300, 200); // 300 pt wide, 200 pt high

        // Dash pattern: 3 pt dash, 2 pt gap
        int[] dashPattern = new int[] { 3, 2 };

        // Border width (points) and color
        int borderWidth = 2;
        Color borderColor = Color.Blue;

        // Helper to create a line with the same dash settings
        void CreateLine(float x1, float y1, float x2, float y2, string contents)
        {
            // borderStyle "D" = Dashed, dashArray supplies the pattern, LEArray not used (null)
            editor.CreateLine(
                rect,                     // annotation rectangle (required but not used for line geometry)
                contents,                 // annotation contents
                x1, y1, x2, y2,           // line start/end coordinates
                1,                        // page number (1‑based)
                borderWidth,              // border width
                borderColor,              // line color
                "D",                      // dashed style
                dashPattern,              // dash pattern
                null);                    // line ending styles (none)
        }

        // Bottom edge
        CreateLine(100, 500, 400, 500, "Bottom edge");
        // Right edge
        CreateLine(400, 500, 400, 700, "Right edge");
        // Top edge
        CreateLine(400, 700, 100, 700, "Top edge");
        // Left edge
        CreateLine(100, 700, 100, 500, "Left edge");

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Dashed rectangle annotation saved to '{outputPath}'.");
    }
}