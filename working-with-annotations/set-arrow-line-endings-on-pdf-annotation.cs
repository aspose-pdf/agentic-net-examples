using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_arrow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the line coordinates
            Point start = new Point(100, 500);
            Point end   = new Point(300, 500);

            // Create a line annotation using the constructor that accepts page, rectangle, start and end points
            LineAnnotation line = new LineAnnotation(
                page,
                new Rectangle(100, 500, 300, 500),
                start,
                end);

            // Set the line geometry (Starting/Ending are the actual points)
            line.Starting = start;
            line.Ending   = end;

            // Set the line ending style – no marker at the start, an open arrow at the end
            line.StartingStyle = LineEnding.None;
            line.EndingStyle   = LineEnding.OpenArrow;

            // Optional visual styling
            line.Color   = Color.Blue;
            line.Opacity = 0.8;

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with arrow line annotation to '{outputPath}'.");
    }
}
