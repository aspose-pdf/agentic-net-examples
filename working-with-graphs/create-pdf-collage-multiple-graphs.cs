using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "collage.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // First graph positioned at (50, 500)
            Graph graph1 = new Graph(200.0, 150.0); // width, height as double
            graph1.Left = 50.0;
            graph1.Top = 500.0;

            // Add a rectangle shape to the first graph
            var rect1 = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 150f);
            rect1.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,
                Color = Color.DarkBlue,
                LineWidth = 2f
            };
            graph1.Shapes.Add(rect1);
            page.Paragraphs.Add(graph1);

            // Second graph positioned at (300, 400)
            Graph graph2 = new Graph(150.0, 150.0);
            graph2.Left = 300.0;
            graph2.Top = 400.0;

            // Add a circle (ellipse) shape to the second graph
            var ellipse = new Ellipse(0f, 0f, 150f, 150f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGreen,
                Color = Color.Green,
                LineWidth = 1.5f
            };
            graph2.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph2);

            // Third graph positioned at (100, 200)
            Graph graph3 = new Graph(250.0, 100.0);
            graph3.Left = 100.0;
            graph3.Top = 200.0;

            // Add a line shape to the third graph
            float[] linePoints = { 0f, 0f, 250f, 0f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 3f
            };
            graph3.Shapes.Add(line);
            page.Paragraphs.Add(graph3);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Collage PDF saved to '{outputPath}'.");
    }
}
