using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Input parameters
        const string outputPath = "star_graph.pdf";
        int numberOfPoints = 7; // Desired star points
        Aspose.Pdf.Color fillColor = Aspose.Pdf.Color.FromRgb(0.9, 0.7, 0.2); // Custom fill color

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Star geometry
            double centerX = 300;
            double centerY = 400;
            double outerRadius = 150;
            double innerRadius = outerRadius * 0.5; // inner radius ratio = 0.5

            // Begin drawing the star using low‑level PDF operators
            page.Contents.Add(new GSave());
            page.Contents.Add(new SetLineWidth(1));
            // Stroke color (black)
            page.Contents.Add(new SetRGBColorStroke(0, 0, 0));
            // Fill color (custom) – use the Data array of Aspose.Pdf.Color
            page.Contents.Add(new SetRGBColor(fillColor.Data[0], fillColor.Data[1], fillColor.Data[2]));

            int totalVertices = numberOfPoints * 2; // outer + inner vertices
            double startAngle = -Math.PI / 2; // start at the top
            double angleStep = Math.PI / numberOfPoints;

            for (int i = 0; i < totalVertices; i++)
            {
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = startAngle + i * angleStep;
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);

                if (i == 0)
                    page.Contents.Add(new MoveTo(x, y));
                else
                    page.Contents.Add(new LineTo(x, y));
            }

            page.Contents.Add(new ClosePathFillStroke());
            page.Contents.Add(new GRestore());

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Star graph saved to '{System.IO.Path.GetFullPath(outputPath)}'.");
    }
}
