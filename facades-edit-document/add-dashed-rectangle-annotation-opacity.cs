using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (Facade) to edit annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the existing PDF.
            editor.BindPdf(inputPath);

            // Page numbers are 1‑based; page 6 is the target.
            Page targetPage = editor.Document.Pages[6];

            // Define rectangle coordinates (llx, lly, urx, ury).
            // Adjust these values as needed for the desired position/size.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a square (rectangle) annotation.
            SquareAnnotation square = new SquareAnnotation(targetPage, rect)
            {
                // Border color.
                Color = Aspose.Pdf.Color.Blue,

                // 50% opacity.
                Opacity = 0.5,

                // Contents (optional tooltip text).
                Contents = "Dashed rectangle annotation"
            };

            // Configure the border: width = 2 points, dashed style.
            // Border constructor requires the parent annotation.
            square.Border = new Border(square)
            {
                Width = 2,
                // Use the BorderStyle enum for dashed lines.
                Style = BorderStyle.Dashed
            };

            // Add the annotation to the page.
            targetPage.Annotations.Add(square);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}