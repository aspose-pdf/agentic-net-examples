using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container (width: 400 pt, height: 300 pt) – use double values as required by the API
            Graph graph = new Graph(400.0, 300.0);

            // ----- Filled rectangle (stroke + fill) -----
            // Drawing.Rectangle constructor: (left, bottom, width, height) – all float values
            var filledRect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 200f, 100f);
            filledRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,   // Fill the rectangle
                Color = Color.Black,           // Stroke color
                LineWidth = 2f                 // Stroke thickness (float)
            };
            graph.Shapes.Add(filledRect);

            // ----- Unfilled circle (stroke only) -----
            var outlineEllipse = new Ellipse(300f, 150f, 100f, 100f);
            outlineEllipse.GraphInfo = new GraphInfo
            {
                // No FillColor => only outline is drawn
                Color = Color.Blue,            // Stroke color
                LineWidth = 3f                 // Stroke thickness
            };
            graph.Shapes.Add(outlineEllipse);

            // ----- Line (stroke only) -----
            // Line constructor takes a float array: { x1, y1, x2, y2 }
            float[] linePoints = { 100f, 50f, 350f, 50f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Red,             // Stroke color
                LineWidth = 1.5f               // Stroke thickness
            };
            graph.Shapes.Add(line);

            // Add the Graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("Graph_StrokeAndFill.pdf");
        }

        Console.WriteLine("PDF with mixed filled and outline shapes created successfully.");
    }
}
