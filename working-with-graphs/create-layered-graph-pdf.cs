using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string outputPath = "LayeredGraph.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Create a Graph (container for drawing shapes)
            // ------------------------------------------------------------
            // Graph constructor expects double values for width and height
            Graph graph = new Graph(500.0, 300.0);

            // Example shape: a light‑gray rectangle with a black border
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                (float)50,   // left
                (float)50,   // bottom
                (float)200,  // width
                (float)100); // height

            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };

            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // 2. Create a PDF layer and add low‑level drawing operators
            // ------------------------------------------------------------
            // Layer constructor: (layerId, layerName)
            Layer layer = new Layer("layer1", "SampleLayer");

            // The Contents property returns an OperatorCollection.
            // Here we draw a red diagonal line using PDF operators.
            layer.Contents.Add(new SetRGBColorStroke(1f, 0f, 0f)); // red stroke
            layer.Contents.Add(new MoveTo(100, 100));
            layer.Contents.Add(new LineTo(400, 250));
            layer.Contents.Add(new Stroke());

            // Add the layer to the page. The page now has two content streams:
            // the default content (the graph) and the optional content group (the layer).
            page.Layers.Add(layer);

            // ------------------------------------------------------------
            // 3. Control layer visibility (optional)
            // ------------------------------------------------------------
            // Locking a layer prevents the user from toggling its visibility in a PDF viewer.
            // Unlocking allows the viewer to show/hide the layer.
            layer.Lock();   // make the layer locked (cannot be turned off by the user)
            // layer.Unlock(); // uncomment to allow the user to toggle visibility

            // ------------------------------------------------------------
            // 4. Save the document
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with layered graph saved to '{outputPath}'.");
    }
}