using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Map shape identifiers to fill colors.
        var shapeFillColors = new Dictionary<string, Color>
        {
            { "rect1", Color.FromRgb(0.9f, 0.2f, 0.2f) },   // Light red
            { "circle1", Color.FromRgb(0.2f, 0.8f, 0.2f) }, // Light green
            { "ellipse1", Color.FromRgb(0.2f, 0.2f, 0.9f) } // Light blue
        };

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a page.
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold the drawing shapes.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // ----- Rectangle -----
            // Rectangle constructor: (left, bottom, width, height)
            // Original coordinates (100,700,250,800) => width = 150, height = 100
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100f, 700f, 150f, 100f);
            rect.GraphInfo.FillColor = shapeFillColors["rect1"];   // Fill
            rect.GraphInfo.Color = Color.Black;                     // Border color
            rect.GraphInfo.LineWidth = 1;                           // Border width
            graph.Shapes.Add(rect);

            // ----- Circle -----
            // Center (350,750) with radius 50
            Circle circle = new Circle(350f, 750f, 50f);
            circle.GraphInfo.FillColor = shapeFillColors["circle1"];
            circle.GraphInfo.Color = Color.Black;
            circle.GraphInfo.LineWidth = 1;
            graph.Shapes.Add(circle);

            // ----- Ellipse -----
            // Original coordinates (450,650,600,750) => width = 150, height = 100
            Ellipse ellipse = new Ellipse(450f, 650f, 150f, 100f);
            ellipse.GraphInfo.FillColor = shapeFillColors["ellipse1"];
            ellipse.GraphInfo.Color = Color.Black;
            ellipse.GraphInfo.LineWidth = 1;
            graph.Shapes.Add(ellipse);

            // Add the graph (which contains all shapes) to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF document.
            doc.Save("shapes_graph.pdf");
        }

        Console.WriteLine("PDF with colored shapes has been created: shapes_graph.pdf");
    }
}
