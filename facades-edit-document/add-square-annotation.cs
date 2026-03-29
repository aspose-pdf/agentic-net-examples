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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("Document does not contain a fifth page.");
                return;
            }

            Page page = doc.Pages[5];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            SquareAnnotation square = new SquareAnnotation(page, rect);
            square.Color = Aspose.Pdf.Color.Red;               // red border
            square.InteriorColor = Aspose.Pdf.Color.Red;       // fill color
            square.Opacity = 0.5f;                             // semi‑transparent fill
            square.Border = new Border(square) { Width = 2 };   // border width
            page.Annotations.Add(square);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Square annotation added and saved to '{outputPath}'.");
    }
}