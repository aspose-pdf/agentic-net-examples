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

        // Load the PDF document
        Document doc = new Document(inputPath);
        // Get the first page (or any page you need)
        Page page = doc.Pages[1];

        // Define the rectangle where the annotation will appear (llx, lly, urx, ury)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a TextAnnotation bound to the page and rectangle
        TextAnnotation annotation = new TextAnnotation(page, rect)
        {
            Title    = "Note",
            Contents = "This is a custom annotation with a thick border.",
            Icon     = TextIcon.Note,
            // Optional: set the annotation's background color for better visibility
            Color    = Color.Yellow
        };

        // Set a solid border with a thickness of 3 points.
        // Border requires the parent annotation in its constructor.
        annotation.Border = new Border(annotation)
        {
            Style = BorderStyle.Solid,
            Width = 3
        };

        // Add the annotation to the page and save the document
        page.Annotations.Add(annotation);
        doc.Save(outputPath);

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
