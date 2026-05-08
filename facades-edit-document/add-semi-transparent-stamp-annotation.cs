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

        // Load the PDF, modify, and save using proper disposal pattern
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Define the annotation rectangle (coordinates are in points, lower‑left origin)
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a StampAnnotation on page 3
            StampAnnotation stamp = new StampAnnotation(doc.Pages[3], annotRect)
            {
                // Set the visual properties of the annotation
                Color    = Aspose.Pdf.Color.Yellow,   // annotation border/color
                Contents = "Semi‑transparent stamp",
                Opacity  = 0.6                         // 60 % opacity
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[3].Annotations.Add(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}