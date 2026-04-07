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
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document has less than 6 pages.");
                return;
            }

            // Define rectangle bounds (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100.0, 500.0, 300.0, 550.0);

            // Create rectangle (square) annotation on page 6
            SquareAnnotation annotation = new SquareAnnotation(doc.Pages[6], rect);
            annotation.Color = Aspose.Pdf.Color.Red;
            annotation.Opacity = 0.5f;

            // Configure dashed border
            Border border = new Border(annotation);
            border.Width = 1;
            border.Dash = new Dash(2, 2);
            annotation.Border = border;

            // Add annotation to the page
            doc.Pages[6].Annotations.Add(annotation);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}