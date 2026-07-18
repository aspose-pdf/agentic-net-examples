using System;
using System.IO;
using System.Drawing; // for Rectangle and Color
using Aspose.Pdf; // core PDF classes
using Aspose.Pdf.Facades; // PdfContentEditor
using Aspose.Pdf.Annotations; // SquareAnnotation, Dash

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

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height) using System.Drawing.Rectangle
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a square (rectangle) annotation.
            // Parameters: rect, contents, color, square(true), pageNumber, borderWidth
            editor.CreateSquareCircle(
                annotRect,
                "Aspose.Pdf.Rectangle annotation",
                System.Drawing.Color.Blue,
                true,
                1,
                2);

            // Retrieve the newly created annotation to apply dash pattern and opacity
            Annotation ann = editor.Document.Pages[1].Annotations[1];

            if (ann is SquareAnnotation squareAnn)
            {
                // Set a custom dash pattern (e.g., 3 units on, 2 units off)
                squareAnn.Border.Dash = new Dash(new int[] { 3, 2 });

                // Set opacity to 75% (0.75)
                squareAnn.Opacity = 0.75;
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Aspose.Pdf.Rectangle annotation added and saved to '{outputPath}'.");
    }
}
