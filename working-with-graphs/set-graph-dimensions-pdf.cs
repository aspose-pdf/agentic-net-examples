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
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph with initial size (width, height) using double literals as required
            Graph graph = new Graph(400.0, 200.0);

            // Set exact dimensions using Width and Height properties (double values)
            graph.Width = 500.0;   // width in points
            graph.Height = 300.0;  // height in points

            // Create a drawing rectangle that matches the graph size
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)graph.Width,
                (float)graph.Height);

            // Configure the rectangle's appearance
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to disk
            doc.Save("GraphDimensions.pdf");
        }

        Console.WriteLine("PDF with custom graph dimensions created successfully.");
    }
}
