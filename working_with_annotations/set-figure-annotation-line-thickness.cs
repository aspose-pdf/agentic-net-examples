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

        // Open the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Use the first page for the example
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a figure annotation – here a SquareAnnotation
            SquareAnnotation figure = new SquareAnnotation(page, rect);

            // Set the line thickness (border width) to 2 points
            // Border requires the parent annotation in its constructor
            figure.Border = new Border(figure) { Width = 2 };

            // Optional: set a visible color for the border
            figure.Color = Aspose.Pdf.Color.Blue;

            // Add the annotation to the page
            page.Annotations.Add(figure);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}