using System;
using System.IO;
using System.Drawing;               // required for Rectangle and Color
using Aspose.Pdf.Facades;          // PdfContentEditor resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "diagram.pdf";
        const string outputPath = "diagram_with_line.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Annotation rectangle (position on the page)
        // Rectangle(x, y, width, height) – coordinates are in points
        Rectangle annotRect = new Rectangle(100, 500, 200, 50);

        // Line start and end coordinates (in points)
        float x1 = 120f, y1 = 520f;
        float x2 = 280f, y2 = 540f;

        // Custom dash pattern: 4‑point dash, 2‑point gap
        int[] dashArray = new int[] { 4, 2 };

        // No special line ending styles
        string[] leArray = new string[] { "None", "None" };

        // Create a dashed line annotation on page 1
        editor.CreateLine(
            annotRect,               // annotation rectangle
            "Workflow step",         // contents (tooltip)
            x1, y1, x2, y2,          // line coordinates
            1,                       // page number (1‑based)
            2,                       // border width in points
            Color.Blue,              // line color (System.Drawing.Color)
            "D",                     // border style: "D" = Dashed
            dashArray,               // custom dash pattern
            leArray);                // line ending styles

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}