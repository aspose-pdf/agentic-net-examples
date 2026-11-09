using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_page6.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Ensure the document has at least 6 pages
        if (doc.Pages.Count < 6)
        {
            Console.Error.WriteLine("The document does not contain a page 6.");
            return;
        }

        // Get page 6 (1‑based index)
        Page page = doc.Pages[6];

        // Define the rectangle bounds (lower‑left (100,500), upper‑right (300,700))
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

        // Create a square (rectangle) annotation. The constructor requires the target page and bounds.
        SquareAnnotation rectAnnotation = new SquareAnnotation(page, rect)
        {
            // Set the fill/border color
            Color = Aspose.Pdf.Color.Blue,
            // Set 50% opacity (value between 0 and 1)
            Opacity = 0.5f
        };

        // Configure the border – Border requires the parent annotation in its constructor.
        rectAnnotation.Border = new Border(rectAnnotation)
        {
            Style = BorderStyle.Dashed,
            Width = 2
        };

        // Add the annotation to the page's annotation collection
        page.Annotations.Add(rectAnnotation);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Rectangle annotation added to page 6 and saved as '{outputPath}'.");
    }
}
