using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle area for the highlight annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a HighlightAnnotation on the first page
            HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[1], rect);

            // Configure the annotation: yellow color and 80% opacity
            highlight.Color   = Aspose.Pdf.Color.Yellow; // yellow fill
            highlight.Opacity = 0.8;                     // 80% opacity

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(highlight);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}