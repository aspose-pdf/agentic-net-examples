using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // SquareAnnotation, Border, Dash, etc.

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (facade) to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF
            editor.BindPdf(inputPath);

            // Define rectangle position (x, y, width, height) using System.Drawing.Rectangle
            // PdfContentEditor.CreateSquareCircle expects a System.Drawing.Rectangle and a System.Drawing.Color
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 150);

            // Create a square (rectangle) annotation on page 1
            // Parameters: rect, contents, color, square=true, page=1, borderWidth=2
            editor.CreateSquareCircle(rect, "Aspose.Pdf.Rectangle annotation", System.Drawing.Color.Red, true, 1, 2);

            // Retrieve the annotation just added (last annotation on page 1)
            Page page = editor.Document.Pages[1];
            // Annotations collection is 0‑based, so use Count - 1
            Annotation ann = page.Annotations[page.Annotations.Count - 1];

            if (ann is SquareAnnotation squareAnn)
            {
                // Set custom dash pattern (e.g., 3 units on, 2 units off)
                squareAnn.Border = new Border(squareAnn)
                {
                    Width = 2,
                    Dash = new Dash(new int[] { 3, 2 })
                };

                // Set opacity to 75%
                squareAnn.Opacity = 0.75f;
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Aspose.Pdf.Rectangle annotation added and saved to '{outputPath}'.");
    }
}
