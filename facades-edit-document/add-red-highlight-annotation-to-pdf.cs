using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define the rectangle area for the annotation (lower‑left X/Y, upper‑right X/Y)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a square (highlight) annotation on page 1
        SquareAnnotation square = new SquareAnnotation(doc.Pages[1], rect);
        square.Title = "Note";
        square.Contents = "Highlighted area";
        // Set the border color to bright red (RGB 255,0,0)
        square.Color = Aspose.Pdf.Color.FromRgb(255, 0, 0);
        // Optionally define border width/style
        square.Border = new Border(square) { Width = 1, Style = BorderStyle.Solid };

        // Add the annotation to the page's annotation collection
        doc.Pages[1].Annotations.Add(square);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Annotation with red border saved to '{outputPath}'.");
    }
}