using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample PDF document
        using (Document doc = new Document())
        {
            // Add a new page
            Page page = doc.Pages.Add();

            // Create a Graph (vector graphic) with width 200 and height 100 points
            Graph graph = new Graph(200.0, 100.0);
            graph.Left = 100.0;
            graph.Top = 500.0;
            graph.Border = new BorderInfo(BorderSide.All, 1.0f);
            graph.GraphInfo = new GraphInfo();
            graph.GraphInfo.Color = Color.Black;

            // Add a rectangle shape to the graph
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(10.0f, 10.0f, 180.0f, 80.0f);
            rect.GraphInfo = new GraphInfo();
            rect.GraphInfo.Color = Color.Blue;
            rect.GraphInfo.FillColor = Color.LightGray;
            graph.Shapes.Add(rect);

            // Embed the graph directly into the page as a vector graphic
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("output.pdf");
        }
    }
}