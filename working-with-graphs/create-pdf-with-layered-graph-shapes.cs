using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "layered_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a new layer (optional content group)
            // Parameters: layer name, layer identifier
            Aspose.Pdf.Layer myLayer = new Aspose.Pdf.Layer("MyLayer", "layer1");

            // Add the layer to the page's layer collection
            page.Layers.Add(myLayer);

            // Create a graph (graphics container) with desired size
            // Width = 400 points, Height = 200 points
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(400, 200);

            // Example shape: a rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 300, 150);
            rect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // Example shape: a line
            float[] linePoints = { 60, 60, 340, 140 };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(line);

            // Add the graph to the page's paragraphs.
            // The graph will be rendered on the page.
            page.Paragraphs.Add(graph);

            // OPTIONAL: Flatten the layer if you want to make its content permanent
            // myLayer.Flatten(false); // false = keep optional content markers for faster processing

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with layer saved to '{outputPath}'.");
    }
}