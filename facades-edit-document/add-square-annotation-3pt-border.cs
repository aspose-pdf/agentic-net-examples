using System;
using Aspose.Pdf;                         // Aspose.Pdf.Color
using Aspose.Pdf.Facades;                 // PdfContentEditor facade

class AddCustomBorderAnnotation
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_annotation.pdf";

        // Ensure the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the Facades API to add a square annotation with a 3‑point border
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height) in points.
            // System.Drawing.Rectangle expects (x, y, width, height).
            // The original Aspose.Pdf.Rectangle (100, 500, 200, 100) has
            // LLX=100, LLY=500, URX=200, URY=100. To avoid a negative height we
            // compute width and height as absolute differences.
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(
                x: 100,
                y: 100,                                 // use the lower Y value for a positive height
                width: 200 - 100,                       // 100 points wide
                height: 500 - 100);                     // 400 points high

            // Create a square annotation with a 3‑point border.
            // The CreateSquareCircle method expects System.Drawing.Rectangle
            // and System.Drawing.Color, so we pass fully‑qualified types.
            editor.CreateSquareCircle(
                rect: annotRect,
                contents: "Annotation with 3‑pt border",
                clr: System.Drawing.Color.Red,
                square: true,
                page: 1,
                borderWidth: 3);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
