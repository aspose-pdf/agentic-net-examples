using System;
using System.IO;
using System.Drawing;                     // System.Drawing types are required by Facades API
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;            // for SquareAnnotation and Border

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

        // Use PdfContentEditor (Facade) to add a rectangle (square) annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height) in points.
            // Facade methods expect System.Drawing.Rectangle.
            // Here we want a rectangle from (100,100) to (200,500).
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 100, 400);

            // Create a square (rectangle) annotation.
            //   contents   – visible text (optional, can be empty)
            //   clr        – border color (System.Drawing.Color)
            //   square     – true for square (rectangle) shape
            //   page       – 1‑based page index
            //   borderWidth– initial border width (will be overridden later)
            editor.CreateSquareCircle(
                rect,
                "Aspose.Pdf.Rectangle annotation",
                System.Drawing.Color.Black,
                true,
                1,
                1);

            // Retrieve the annotation just added (last annotation on page 1)
            Page page = editor.Document.Pages[1];
            // Annotations collection is zero‑based, so use Count‑1
            Annotation ann = page.Annotations[page.Annotations.Count - 1];
            if (ann is SquareAnnotation squareAnn)
            {
                // Set fill (interior) color to light gray (Aspose.Pdf.Color)
                squareAnn.InteriorColor = Aspose.Pdf.Color.LightGray;

                // Set border width to 1 pt via Border object (requires parent annotation)
                squareAnn.Border = new Aspose.Pdf.Annotations.Border(squareAnn) { Width = 1 };
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Aspose.Pdf.Rectangle annotation added and saved to '{outputPath}'.");
    }
}
