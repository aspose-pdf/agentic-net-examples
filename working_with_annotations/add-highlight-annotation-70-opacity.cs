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
            // Define the rectangle area for the highlight annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation on the first page
            HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[1], rect)
            {
                Opacity = 0.7 // 70% opacity for subtle emphasis
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added with 70% opacity. Saved to '{outputPath}'.");
    }
}