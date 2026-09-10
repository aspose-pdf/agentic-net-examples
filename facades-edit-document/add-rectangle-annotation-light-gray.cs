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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor facade with the loaded document
            PdfAnnotationEditor annotEditor = new PdfAnnotationEditor(doc);

            // Define the rectangle area for the annotation using Aspose.Pdf.Rectangle
            // Lower‑left (100, 500), width 200, height 100 => upper‑right (300, 600)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a square (rectangle) annotation on page 1
            SquareAnnotation square = new SquareAnnotation(doc.Pages[1], rect);

            // Set the interior (fill) color to light gray
            square.InteriorColor = Aspose.Pdf.Color.LightGray;

            // Set the border width to 1 pt
            square.Border = new Border(square) { Width = 1 };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(square);

            // Save the modified PDF via the facade (lifecycle rule: use Save on the facade)
            annotEditor.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation saved to '{outputPath}'.");
    }
}
