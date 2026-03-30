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
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a square annotation
            SquareAnnotation square = new SquareAnnotation(page, rect);

            // Configure custom border (3 points thick, solid style)
            // Border.Width expects an integer, so use an int literal (or cast explicitly)
            square.Border = new Border(square)
            {
                Width = 3,               // integer value for thickness
                Style = BorderStyle.Solid
            };

            // Optional: set fill color for visibility
            square.Color = Aspose.Pdf.Color.Yellow;

            page.Annotations.Add(square);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation with custom border saved to '{outputPath}'.");
    }
}
