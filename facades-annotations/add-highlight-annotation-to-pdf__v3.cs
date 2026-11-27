using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdf = new Document(inputPath);

        // Define the area to highlight (llx, lly, urx, ury)
        Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

        // HighlightAnnotation requires the target page and the rectangle
        HighlightAnnotation highlight = new HighlightAnnotation(pdf.Pages[1], highlightRect)
        {
            Color = Aspose.Pdf.Color.Yellow
        };
        pdf.Pages[1].Annotations.Add(highlight);

        // Save the modified PDF
        pdf.Save(outputPath);
        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}
