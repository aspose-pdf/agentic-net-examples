using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "LineAdded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            // Bounding rectangle for the annotation (required by the API)
            Rectangle rect = new Rectangle(100, 200, 300, 400);

            // Create the line annotation – constructor requires page, rectangle, start point and end point
            LineAnnotation line = new LineAnnotation(page, rect,
                new Point(100, 200),   // start point
                new Point(300, 400));  // end point

            // Arrow at the start of the line
            line.StartingStyle = LineEnding.OpenArrow;
            line.EndingStyle   = LineEnding.None;

            // Set line width (and optionally colour) via the Border object
            line.Border = new Border(line) { Width = 2 };
            line.Color = Color.Black;

            page.Annotations.Add(line);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}
