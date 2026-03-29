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
            // Define the rectangle that will be highlighted
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the highlight annotation
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect);

            // Set the fill colour of the highlight (optional)
            highlight.Color = Aspose.Pdf.Color.Yellow;

            // Configure the border – the border colour is taken from the annotation's own Color property.
            // Therefore we set the annotation's Color to the desired border colour (bright red).
            highlight.Color = Aspose.Pdf.Color.FromRgb(255, 0, 0); // border colour
            highlight.Border = new Border(highlight) { Width = 1 };

            // Add the annotation to the page and save the document
            page.Annotations.Add(highlight);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved highlighted PDF with red border to '{outputPath}'.");
    }
}
