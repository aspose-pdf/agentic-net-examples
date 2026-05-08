using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "line_graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Optional: remove page margins to use full page area
            page.PageInfo.Margin = new MarginInfo(0, 0, 0, 0);

            // Define line appearance
            // Set line cap style (RoundCap, ButtCap, SquareCap)
            page.Contents.Add(new SetLineCap(LineCap.RoundCap));

            // Set line color (red) and width (2 points)
            page.Contents.Add(new SetRGBColorStroke(255, 0, 0)); // red
            page.Contents.Add(new SetLineWidth(2));

            // Define start and end points (in points, origin at lower‑left)
            float startX = 100f;
            float startY = 200f;
            float endX   = 400f;
            float endY   = 500f;

            // Move to start point
            page.Contents.Add(new MoveTo(startX, startY));
            // Draw line to end point
            page.Contents.Add(new LineTo(endX, endY));
            // Stroke the path (render the line)
            page.Contents.Add(new Stroke());

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with line saved to '{outputPath}'.");
    }
}