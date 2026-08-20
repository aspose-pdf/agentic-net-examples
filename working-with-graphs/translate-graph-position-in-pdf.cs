using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Graph constructor expects double values (the older float overload is obsolete)
            Graph graph = new Graph(400.0, 200.0);

            // Position the graph at (100, 500) on the page
            graph.Left = 100;
            graph.Top = 500;

            // Add a rectangle shape inside the graph
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle) for drawing shapes
            Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 150, 80);
            shape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(shape);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Translate (move) the entire graph horizontally by 30 points
            // and vertically by 20 points relative to its current location
            double dx = 30;
            double dy = 20;
            graph.Left += dx; // horizontal translation
            graph.Top += dy;  // vertical translation

            // Save the resulting PDF
            doc.Save("translated_graph.pdf");
        }
    }
}
