using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDoc = new Document(inputPath);

        // Define the annotation rectangle (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
        // Fully‑qualified type to avoid ambiguity with System.Drawing.Rectangle
        Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

        // Create a square annotation – the constructor expects the parent Page and the rectangle
        SquareAnnotation square = new SquareAnnotation(pdfDoc.Pages[1], annotRect);
        square.Contents = "Custom border annotation";
        square.Color = Aspose.Pdf.Color.Blue;
        // Border also requires the parent annotation in its constructor
        square.Border = new Border(square) { Width = 3 };

        // Add the annotation to the first page (pages are 1‑based)
        pdfDoc.Pages[1].Annotations.Add(square);

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Annotation added with 3‑point border. Saved to '{outputPath}'.");
    }
}
