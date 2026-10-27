using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class StarShapeExample
{
    static void Main()
    {
        const string outputPath = "star.pdf";

        // Parameters for the star
        int starPoints = 5;          // number of star points
        double outerRadius = 200;    // radius of outer vertices
        double innerRadius = 80;     // radius of inner vertices
        double centerX = 300;        // X coordinate of star center
        double centerY = 400;        // Y coordinate of star center

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph that will hold the star shape
            Graph graph = new Graph(600, 800);

            // Build the point list for the star (closed Line)
            var pointList = new System.Collections.Generic.List<float>();
            for (int i = 0; i < starPoints * 2; i++)
            {
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = Math.PI / starPoints * i - Math.PI / 2; // start at top
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);
                pointList.Add((float)x);
                pointList.Add((float)y);
            }
            // Close the polygon by repeating the first point
            pointList.Add(pointList[0]);
            pointList.Add(pointList[1]);

            // Create the closed Line (acts as a polygon)
            Line star = new Line(pointList.ToArray());
            // Set fill color (gold) via GraphInfo
            star.GraphInfo.FillColor = Aspose.Pdf.Color.FromRgb(1.0, 0.84, 0.0);

            // Add the star shape to the graph
            graph.Shapes.Add(star);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Star PDF saved to {outputPath}");
    }
}
