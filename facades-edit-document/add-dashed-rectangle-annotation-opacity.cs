using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Define the rectangle bounds (x, y, width, height) using System.Drawing.Rectangle.
            var annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Add a square (rectangle) annotation on page 6.
            // Parameters: rect, contents, color, square=true, pageNumber, borderWidth
            editor.CreateSquareCircle(
                annotRect,
                "Aspose.Pdf.Rectangle annotation",
                System.Drawing.Color.Red,
                true,
                6,
                2);

            // Retrieve the underlying Document to modify annotation properties.
            Document doc = editor.Document;
            Page page = doc.Pages[6];

            if (page.Annotations.Count > 0)
            {
                // The newly added annotation is the last one on the page.
                Annotation lastAnn = page.Annotations[page.Annotations.Count];

                if (lastAnn is SquareAnnotation squareAnn)
                {
                    // Set 50% opacity.
                    squareAnn.Opacity = 0.5;

                    // Configure the border: dashed style with a dash pattern.
                    squareAnn.Border = new Border(squareAnn)
                    {
                        Style = BorderStyle.Dashed,
                        Dash = new Dash(new int[] { 3, 3 })
                    };
                }
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Aspose.Pdf.Rectangle annotation added and saved to '{outputPath}'.");
    }
}
