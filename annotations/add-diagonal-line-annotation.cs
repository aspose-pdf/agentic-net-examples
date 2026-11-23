using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "DiagonalLine.pdf";   // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page
            Page page = doc.Pages[1];

            // Rectangle that defines the annotation's bounding box (required by API)
            var rect = new Rectangle(50, 50, 400, 400);

            // Create a line annotation from (50,50) to (400,400)
            var line = new LineAnnotation(page, rect,
                new Point(50, 50), new Point(400, 400))
            {
                Color = Color.Black,               // line colour
                StartingStyle = LineEnding.None,   // no arrow at start
                EndingStyle = LineEnding.OpenArrow // open arrow at end
            };

            // Set line width via the Border object
            line.Border = new Border(line) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}
