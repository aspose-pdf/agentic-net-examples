using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Define start and end points of the line (relative to the page)
            Point start = new Point(120, 520);
            Point end   = new Point(280, 530);

            // Create the line annotation
            LineAnnotation line = new LineAnnotation(page, rect, start, end);

            // Set line ending styles: circle at the start, open arrow at the end
            line.StartingStyle = LineEnding.Circle;      // circle for start point
            line.EndingStyle   = LineEnding.OpenArrow;   // arrow for end point

            // Optional visual styling
            line.Color = Aspose.Pdf.Color.Blue;
            line.InteriorColor = Aspose.Pdf.Color.LightGray;
            line.Border = new Border(line) { Width = 1 };

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation saved to '{outputPath}'.");
    }
}