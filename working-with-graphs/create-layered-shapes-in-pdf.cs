using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a new layer and add it to the page's layer collection
            // The first parameter is the layer name, the second is an optional content group ID
            Layer myLayer = new Layer("MyLayer", "OCG1");
            page.Layers.Add(myLayer);

            // Create a Graph object (acts like a container for drawing shapes)
            // Width = 400 points, Height = 200 points
            Graph graph = new Graph(400, 200)
            {
                // Optional: set overall graph styling
                GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,
                    LineWidth = 1
                }
            };

            // ----- Add a rectangle shape -----
            // Rectangle(left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 100, 50);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rect);

            // ----- Add an ellipse shape -----
            // Ellipse(left, bottom, width, height)
            Ellipse ellipse = new Ellipse(200, 50, 100, 100);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1
            };
            graph.Shapes.Add(ellipse);

            // ----- Add a line shape -----
            // Line constructor takes an array: { x1, y1, x2, y2 }
            float[] linePoints = { 50, 150, 300, 150 };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Green,
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Add the graph (with all its shapes) to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Optional: flatten the layer if you want its content merged into the page content
            // Pass 'false' to keep optional content group markers (faster)
            // myLayer.Flatten(false);

            // Save the PDF to a file
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with layered graphics created successfully.");
    }
}