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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page
            Page page = doc.Pages[1];

            // Define the annotation rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a square annotation on the first page
            SquareAnnotation square = new SquareAnnotation(page, rect);
            // Set the border color (the annotation's Color property controls border color)
            square.Color = Aspose.Pdf.Color.Red;
            // Set a custom border thickness of three points – Border requires the parent annotation
            square.Border = new Border(square) { Width = 3 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
