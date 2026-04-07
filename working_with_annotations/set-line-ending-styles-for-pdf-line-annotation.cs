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

        // Wrap Document in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Define start and end points of the line
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(120, 520);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(280, 530);

            // Create a line annotation on the page
            LineAnnotation line = new LineAnnotation(page, rect, start, end);

            // Set line ending styles: circle at the start, open arrow at the end
            line.StartingStyle = Aspose.Pdf.Annotations.LineEnding.Circle;
            line.EndingStyle   = Aspose.Pdf.Annotations.LineEnding.OpenArrow;

            // Optional visual styling
            line.Color = Aspose.Pdf.Color.Blue;
            line.Border = new Border(line) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}