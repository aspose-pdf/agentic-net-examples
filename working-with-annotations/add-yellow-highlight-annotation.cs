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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle area for the highlight annotation (example coordinates)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a HighlightAnnotation on the first page
            HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[1], rect);

            // Set the annotation color to yellow
            highlight.Color = Aspose.Pdf.Color.Yellow;

            // Set the annotation opacity to 80%
            highlight.Opacity = 0.8;

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}