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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle that covers the target paragraph.
            // Adjust the coordinates (llx, lly, urx, ury) as needed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a strikeout annotation on the first page.
            StrikeOutAnnotation strike = new StrikeOutAnnotation(doc.Pages[1], rect)
            {
                Title = "reviewer",               // Author of the annotation
                Color = Aspose.Pdf.Color.Red,    // Visual color of the strikeout line
                Opacity = 0.5                     // Optional opacity
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(strike);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strikethrough annotation added. Saved to '{outputPath}'.");
    }
}