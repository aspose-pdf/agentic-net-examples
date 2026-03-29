using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (covers the line area)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Define start and end points of the line
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(120, 520);
            Aspose.Pdf.Point end = new Aspose.Pdf.Point(280, 580);

            // Create the line annotation on the page
            LineAnnotation line = new LineAnnotation(page, rect, start, end);
            line.Color = Aspose.Pdf.Color.Blue;
            line.StartingStyle = LineEnding.ClosedArrow; // arrowhead at the start
            line.EndingStyle = LineEnding.ClosedArrow;   // arrowhead at the end
            line.Border = new Border(line) { Width = 2 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(line);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("Line annotation with arrowheads saved to '" + outputPath + "'.");
    }
}