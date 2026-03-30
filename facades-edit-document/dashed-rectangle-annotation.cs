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

            // Configure a dashed border
            Border border = new Border(square);
            border.Style = BorderStyle.Dashed;
            border.Width = 2;
            border.Dash = new Dash(4, 2); // dash length 4, gap length 2
            square.Border = border;
            square.Color = Aspose.Pdf.Color.Blue;

            page.Annotations.Add(square);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with dashed rectangle annotation to '{outputPath}'.");
    }
}