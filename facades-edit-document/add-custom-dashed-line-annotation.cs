using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color (PdfContentEditor API)
using Aspose.Pdf.Facades;          // Facade API for creating annotations

class Program
{
    static void Main()
    {
        const string inputPath  = "diagram.pdf";          // Source PDF containing the diagram
        const string outputPath = "diagram_with_line.pdf"; // Destination PDF

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfContentEditor and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Annotation rectangle – can be zero-sized because the line is defined by coordinates
        Rectangle annotRect = new Rectangle(0, 0, 0, 0);

        // Define line start and end points (adjust as needed for your workflow step)
        float x1 = 100f; // start X
        float y1 = 200f; // start Y
        float x2 = 300f; // end X
        float y2 = 200f; // end Y

        // Border width (in points)
        int borderWidth = 2;

        // Line color
        Color lineColor = Color.Blue;

        // Border style: "D" indicates a dashed line
        string borderStyle = "D";

        // Custom dash pattern: 5 units dash, 3 units gap
        int[] dashArray = new int[] { 5, 3 };

        // Line ending styles – both set to "None" (no arrowheads)
        string[] leArray = new string[] { "None", "None" };

        // Create the line annotation on page 1
        editor.CreateLine(
            annotRect,               // Annotation rectangle
            "Workflow step",         // Contents (tooltip)
            x1, y1, x2, y2,         // Coordinates of the line
            1,                       // Page number (1‑based indexing)
            borderWidth,             // Border width
            lineColor,               // Line color
            borderStyle,             // Border style ("D" for dashed)
            dashArray,               // Custom dash pattern
            leArray);                // Line ending styles

        // Save the modified PDF
        editor.Save(outputPath);
    }
}