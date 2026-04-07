using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "collage.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // First graph: a rectangle positioned at (50, 500)
            // -------------------------------------------------
            Graph graph1 = new Graph(200.0, 150.0); // width, height as double
            graph1.Left = 50.0;   // X coordinate on the page
            graph1.Top = 500.0;   // Y coordinate on the page

            var rect1 = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 150f);
            rect1.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph1.Shapes.Add(rect1);
            page.Paragraphs.Add(graph1);

            // -------------------------------------------------
            // Second graph: an ellipse positioned at (300, 400)
            // -------------------------------------------------
            Graph graph2 = new Graph(150.0, 200.0);
            graph2.Left = 300.0;
            graph2.Top = 400.0;

            var ellipse = new Ellipse(0f, 0f, 150f, 200f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph2.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph2);

            // -------------------------------------------------
            // Third graph: a horizontal line positioned at (100, 200)
            // -------------------------------------------------
            Graph graph3 = new Graph(250.0, 100.0);
            graph3.Left = 100.0;
            graph3.Top = 200.0;

            var line = new Line(new float[] { 0f, 0f, 250f, 0f });
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2f
            };
            graph3.Shapes.Add(line);
            page.Paragraphs.Add(graph3);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Collage PDF saved to '{outputPath}'.");
    }
}
