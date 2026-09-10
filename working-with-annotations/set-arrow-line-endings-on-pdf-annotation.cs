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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Define the annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Define start and end points of the line
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(120, 520);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(280, 530);

            // Create a line annotation on the first page
            LineAnnotation line = new LineAnnotation(doc.Pages[1], rect, start, end);

            // Set visual properties
            line.Color = Aspose.Pdf.Color.Blue;

            // Set arrow line endings: open arrow at start, closed arrow at end
            line.StartingStyle = Aspose.Pdf.Annotations.LineEnding.OpenArrow;
            line.EndingStyle   = Aspose.Pdf.Annotations.LineEnding.ClosedArrow;

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}