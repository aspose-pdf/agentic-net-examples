using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Rectangle and Color are defined here

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

        // Initialize the facade that works with PDF annotations
        PdfContentEditor editor = new PdfContentEditor();

        // Load the existing PDF document
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (position and size)
        // Rectangle(x, y, width, height) – coordinates are in points
        Rectangle rect = new Rectangle(100, 500, 200, 200);

        // Create a circle annotation on page 2
        // Parameters: rect, contents, fill color, square (false = circle), page number, border width
        editor.CreateSquareCircle(
            rect,
            "Circle annotation",          // annotation contents
            Color.Green,                  // fill color (green)
            false,                        // false => circle shape
            2,                            // page number (1‑based indexing)
            3);                           // border width in points

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Circle annotation added to page 2 and saved as '{outputPath}'.");
    }
}