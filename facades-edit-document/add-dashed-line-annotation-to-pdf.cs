using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "diagram.pdf";
        const string outputPath = "diagram_annotated.pdf";

        // Verify the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the editor
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Annotation rectangle (zero‑size is acceptable for line annotations)
        Rectangle annotRect = new Rectangle(0, 0, 0, 0);

        // Define line start and end points (in points)
        float x1 = 100f, y1 = 500f, x2 = 300f, y2 = 500f;

        // Custom dash pattern: 4 units dash, 2 units gap
        int[] dashArray = new int[] { 4, 2 };

        // No special line ending styles
        string[] leArray = new string[] { "None", "None" };

        // Create the line annotation on page 1
        editor.CreateLine(
            annotRect,
            "Workflow step highlight", // annotation contents
            x1, y1, x2, y2,
            1,                         // page number (1‑based)
            2,                         // border width in points
            Color.Blue,                // line color
            "D",                       // border style = Dashed
            dashArray,
            leArray);

        // Save the annotated PDF
        editor.Save(outputPath);

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}