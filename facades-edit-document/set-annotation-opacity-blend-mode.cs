using System;
using System.IO;
using Aspose.Pdf;
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

        using (Document doc = new Document(inputPath))
        {
            // Create a square annotation on the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SquareAnnotation annotation = new SquareAnnotation(doc.Pages[1], rect);

            // Set visual appearance
            annotation.Color = Aspose.Pdf.Color.Yellow;
            annotation.Opacity = 0.75; // 75% opacity
            // NOTE: BlendMode property is not available on SquareAnnotation in the current Aspose.Pdf version.
            // If a later version adds this property, you can enable it with:
            // annotation.BlendMode = Aspose.Pdf.BlendMode.Multiply;

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation with opacity saved to '{outputPath}'.");
    }
}
