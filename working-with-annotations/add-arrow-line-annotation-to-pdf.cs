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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Define start and end points of the line
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(120, 520);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(280, 520);

            // Create a line annotation and set arrow line endings
            LineAnnotation line = new LineAnnotation(page, rect, start, end)
            {
                Color = Aspose.Pdf.Color.Blue,                                   // line color
                StartingStyle = Aspose.Pdf.Annotations.LineEnding.ClosedArrow,    // arrow at start
                EndingStyle   = Aspose.Pdf.Annotations.LineEnding.ClosedArrow,    // arrow at end
                InteriorColor = Aspose.Pdf.Color.LightGray                       // fill color for closed arrows
            };

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}