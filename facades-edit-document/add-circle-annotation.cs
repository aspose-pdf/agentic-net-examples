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
            // Page six (1‑based indexing)
            Page page = doc.Pages[6];

            // Define rectangle around the diagram (example coordinates)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // Create circle annotation with green outline
            CircleAnnotation circle = new CircleAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Green
            };

            // Set a thick border (width = 5)
            circle.Border = new Border(circle) { Width = 5 };

            // Add the annotation to the page
            page.Annotations.Add(circle);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation saved to '{outputPath}'.");
    }
}
