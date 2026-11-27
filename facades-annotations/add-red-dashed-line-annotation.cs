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

        // Load the PDF document.
        Document pdf = new Document(inputPath);

        // Ensure the document has at least 2 pages.
        if (pdf.Pages.Count < 2)
        {
            Console.Error.WriteLine("The PDF does not contain a page 2.");
            return;
        }

        // Page on which the annotation will be placed.
        Page page = pdf.Pages[2];

        // Rectangle that encloses the line (coordinates are in points).
        var rect = new Rectangle(100, 500, 300, 500);

        // Create the line annotation.
        var line = new LineAnnotation(page, rect,
                                      new Point(100, 500),   // start point
                                      new Point(300, 500))   // end point
        {
            Color = Color.Red,
            StartingStyle = LineEnding.None,
            EndingStyle = LineEnding.None
        };

        // Set line width and dash style via the Border object.
        line.Border = new Border(line)
        {
            Width = 2,
            Style = BorderStyle.Dashed
        };

        // Add the annotation to the page.
        page.Annotations.Add(line);

        // Save the modified PDF.
        pdf.Save(outputPath);

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}
