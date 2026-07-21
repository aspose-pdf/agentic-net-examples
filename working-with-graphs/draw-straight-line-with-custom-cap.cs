using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define start and end points for the line
            double startX = 100;
            double startY = 500;
            double endX = 300;
            double endY = 500;

            // Set the line cap style (e.g., RoundCap)
            page.Contents.Add(new SetLineCap(LineCap.RoundCap));

            // Move to the start point
            page.Contents.Add(new MoveTo(startX, startY));

            // Draw line to the end point
            page.Contents.Add(new LineTo(endX, endY));

            // Stroke the path (render the line)
            page.Contents.Add(new Stroke());

            // Save the PDF
            string outputPath = "line_graph.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF with line saved to '{outputPath}'.");
        }
    }
}