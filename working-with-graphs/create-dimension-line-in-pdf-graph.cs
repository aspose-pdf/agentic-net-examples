using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400 points, height: 200 points)
            // Use double parameters as the constructor overload with float is obsolete
            Graph graph = new Graph(400.0, 200.0);

            // Define a line that starts at (50,150) and ends at (350,150)
            // PositionArray expects an array of coordinates: { x1, y1, x2, y2, ... }
            float[] linePos = { 50f, 150f, 350f, 150f };
            Line line = new Line(linePos);

            // Style the line via GraphInfo (color and thickness)
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,   // line color
                LineWidth = 2f                    // line thickness
            };

            // Add the line to the graph's shape collection
            graph.Shapes.Add(line);

            // Set a title for the graph – Graph.Title expects a TextFragment, not a string
            graph.Title = new TextFragment("Dimension Line Example");

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF (no SaveOptions needed – will always produce PDF)
            doc.Save("DimensionLine.pdf");
        }

        Console.WriteLine("PDF with dimension line created: DimensionLine.pdf");
    }
}
