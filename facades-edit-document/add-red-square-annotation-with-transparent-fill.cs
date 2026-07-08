using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Rectangle and Color (required by PdfContentEditor)
using Aspose.Pdf;                         // Core PDF classes
using Aspose.Pdf.Facades;                 // PdfContentEditor (facade API)
using Aspose.Pdf.Annotations;             // Annotation classes

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output.pdf";         // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF and add a square annotation on page 5
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height)
            // Note: System.Drawing.Rectangle is required by the facade method.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 200);

            // Create a square annotation:
            //   contents   – text shown when the annotation is selected
            //   clr        – colour of the shape (used for border colour)
            //   square     – true => square, false => circle
            //   page       – target page (1‑based indexing)
            //   borderWidth– thickness of the border
            editor.CreateSquareCircle(
                rect,
                "Square annotation",
                System.Drawing.Color.Red,   // border colour (System.Drawing.Color)
                true,                       // square shape
                5,                          // page number
                2);                         // border width

            // After creation, adjust fill colour and opacity via the concrete SquareAnnotation object.
            // The annotation is the first (and only) one on page 5.
            // Annotations collection is zero‑based.
            SquareAnnotation ann = (SquareAnnotation)editor.Document.Pages[5].Annotations[0];

            // Set border colour using Aspose.Pdf.Color (cross‑platform)
            ann.Color = Aspose.Pdf.Color.Red;

            // Set interior (fill) colour – semi‑transparent red
            ann.InteriorColor = Aspose.Pdf.Color.Red;

            // Make the fill semi‑transparent (0 = fully transparent, 1 = opaque)
            ann.Opacity = 0.5f;

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Square annotation added and saved to '{outputPath}'.");
    }
}
