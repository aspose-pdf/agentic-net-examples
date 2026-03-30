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
            SquareAnnotation annotation = new SquareAnnotation(page, rect);
            annotation.Color = Aspose.Pdf.Color.Blue;
            annotation.Border = new Border(annotation)
            {
                Width = 2,
                Dash = new Dash(new int[] { 3, 2 })
            };
            annotation.Opacity = 0.75f;
            page.Annotations.Add(annotation);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation saved to '{outputPath}'.");
    }
}