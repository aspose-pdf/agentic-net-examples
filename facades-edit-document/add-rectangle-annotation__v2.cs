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
            SquareAnnotation square = new SquareAnnotation(page, rect);
            square.InteriorColor = Aspose.Pdf.Color.LightGray;
            square.Border = new Border(square) { Width = 1 };
            page.Annotations.Add(square);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}