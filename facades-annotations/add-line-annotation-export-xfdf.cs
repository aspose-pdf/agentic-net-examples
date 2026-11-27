using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string annotatedPath = "annotated.pdf";
        const string xfdfPath = "annotations.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add a line annotation to the first page and save it
        using (Document doc = new Document(inputPath))
        {
            // Define start and end points of the line
            Point start = new Point(100, 500);
            Point end   = new Point(300, 500);

            // Create the line annotation (constructor requires Document and two points)
            LineAnnotation line = new LineAnnotation(doc, start, end)
            {
                Color = Color.Red,
                StartingStyle = LineEnding.None,
                EndingStyle   = LineEnding.OpenArrow
            };

            // Add the annotation to page 1
            doc.Pages[1].Annotations.Add(line);

            // Save the PDF that now contains the annotation
            doc.Save(annotatedPath);

            // Export all annotations from the same Document to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{annotatedPath}'.");
        Console.WriteLine($"Annotations exported to XFDF file '{xfdfPath}'.");
    }
}
