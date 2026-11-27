using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Define the annotation rectangle (lower‑left X,Y and upper‑right X,Y) in points.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

        // Get the target page (pages are 1‑based).
        Page page = doc.Pages[2];

        // Create a text annotation on page 2 using the (Page, Rectangle) constructor.
        TextAnnotation textAnn = new TextAnnotation(page, rect)
        {
            Contents = "Sample note",
            Color = Aspose.Pdf.Color.Red
        };

        // Add the annotation to the page.
        page.Annotations.Add(textAnn);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}
