using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "workflow.pdf";
        const string outputPath = "workflow_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the Facades editor and bind the source PDF.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (position is not used for line drawing,
        // so a zero‑size rectangle is acceptable).
        Rectangle annotRect = new Rectangle(0, 0, 0, 0);

        // Line coordinates (start and end points) in user space.
        float x1 = 100f; // start X
        float y1 = 200f; // start Y
        float x2 = 300f; // end X
        float y2 = 200f; // end Y

        // Border width (in points) and color.
        int borderWidth = 1;
        Color lineColor = Color.Red;

        // Use a dashed border style and provide a custom dash pattern.
        string borderStyle = "D";                     // "D" = Dashed
        int[] dashPattern = new int[] { 4, 2 };       // 4 units dash, 2 units gap

        // Optional line ending styles (none in this case).
        string[] lineEndings = new string[] { "None", "None" };

        // Create the line annotation on page 1.
        editor.CreateLine(
            annotRect,
            "Workflow Step Highlight", // annotation contents (tooltip)
            x1, y1, x2, y2,
            page: 1,
            border: borderWidth,
            clr: lineColor,
            borderStyle: borderStyle,
            dashArray: dashPattern,
            LEArray: lineEndings);

        // Save the modified PDF.
        editor.Save(outputPath);
        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}