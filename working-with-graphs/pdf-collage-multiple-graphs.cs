using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "collage.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page (default size A4)
            Page page = doc.Pages.Add();

            // ---------- First Graph ----------
            // Position: top‑left corner
            Graph graph1 = new Graph(200.0, 150.0); // width, height as double
            graph1.Left = 50;   // X coordinate from page left
            graph1.Top  = 700;  // Y coordinate from page bottom

            // Add a rectangle shape
            var rect1 = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 150f);
            rect1.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 2f
            };
            graph1.Shapes.Add(rect1);

            // Add the first graph to the page
            page.Paragraphs.Add(graph1);

            // ---------- Second Graph ----------
            // Position: top‑right corner
            Graph graph2 = new Graph(180.0, 130.0);
            graph2.Left = 350;
            graph2.Top  = 720;

            // Add an ellipse shape
            Ellipse ellipse = new Ellipse(0f, 0f, 180f, 130f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Red,
                LineWidth = 1.5f
            };
            graph2.Shapes.Add(ellipse);

            page.Paragraphs.Add(graph2);

            // ---------- Third Graph ----------
            // Position: bottom‑left corner
            Graph graph3 = new Graph(220.0, 120.0);
            graph3.Left = 40;
            graph3.Top  = 100;

            // Add a line shape
            float[] linePoints = { 0f, 0f, 220f, 0f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Color.Blue,
                LineWidth = 3f
            };
            graph3.Shapes.Add(line);

            page.Paragraphs.Add(graph3);

            // ---------- Fourth Graph ----------
            // Position: bottom‑right corner
            Graph graph4 = new Graph(160.0, 160.0);
            graph4.Left = 350;
            graph4.Top  = 100;

            // Combine rectangle and ellipse in one graph
            var rect2 = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 160f, 80f);
            rect2.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGreen,
                Color     = Color.DarkGreen,
                LineWidth = 1f
            };
            graph4.Shapes.Add(rect2);

            Ellipse ellipse2 = new Ellipse(0f, 80f, 160f, 80f);
            ellipse2.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,
                Color     = Color.Navy,
                LineWidth = 1f
            };
            graph4.Shapes.Add(ellipse2);

            page.Paragraphs.Add(graph4);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Collage PDF saved to '{outputPath}'.");
    }
}
