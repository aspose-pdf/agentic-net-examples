using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle area to be highlighted (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation on the first page
            HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[1], rect);

            // Set opacity to 70% (0.7) for subtle emphasis
            highlight.Opacity = 0.7;

            // Optional: set a visible color for the highlight
            highlight.Color = Aspose.Pdf.Color.Yellow;

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(highlight);

            // Save the modified PDF (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}