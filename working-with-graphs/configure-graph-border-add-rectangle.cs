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
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt) using double parameters
            Graph graph = new Graph(400.0, 200.0);

            // Configure the Graph's line width and dash style (these settings are inherited by shapes)
            graph.GraphInfo.LineWidth = 2f; // 2 points
            graph.GraphInfo.DashArray = new int[] { 5, 2 };

            // Create a rectangle shape inside the graph
            // Parameters: left, bottom, width, height (float values)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 100f);

            // Optional: override rectangle's own GraphInfo (inherits from GraphInfo if not set)
            rect.GraphInfo.LineWidth = 2f;
            rect.GraphInfo.DashArray = new int[] { 5, 2 };
            rect.GraphInfo.Color = Aspose.Pdf.Color.Blue;          // border color
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray; // fill color

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("CustomBorderGraph.pdf");
        }

        Console.WriteLine("PDF with custom graph border and rectangle saved as 'CustomBorderGraph.pdf'.");
    }
}