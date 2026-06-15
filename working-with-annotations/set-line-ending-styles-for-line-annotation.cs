using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Define start and end points of the line
            Point start = new Point(120, 520);
            Point end   = new Point(280, 520);

            // Create a line annotation on the page
            LineAnnotation line = new LineAnnotation(page, rect, start, end);

            // Set line ending styles: circle at the start, arrow at the end
            line.StartingStyle = LineEnding.Circle;      // circle marker at start point
            line.EndingStyle   = LineEnding.OpenArrow;   // arrow marker at end point

            // Optional: set the line color for visibility
            line.Color = Color.Blue;

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the PDF
            doc.Save("output.pdf");
        }
    }
}