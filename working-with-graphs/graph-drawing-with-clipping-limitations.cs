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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangular region that will act as a clipping area.
            // This rectangle is only used to illustrate the intended clipping bounds.
            // Aspose.Pdf.Graph does not have a Clip property, so we cannot set it directly.
            // Instead, we will rely on the Page.AddGraphics method (which accepts a rectangle)
            // to restrict drawing to this area when adding graphic elements.
            Aspose.Pdf.Rectangle clippingRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a Graph object – a container for drawing shapes.
            Graph graph = new Graph(400, 200);

            // ----- Add a rectangle shape -----
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            rectShape.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rectShape);

            // ----- Add an ellipse shape -----
            Aspose.Pdf.Drawing.Ellipse ellipseShape = new Aspose.Pdf.Drawing.Ellipse(250, 0, 350, 100);
            ellipseShape.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page's paragraph collection.
            // This draws the shapes without any explicit clipping.
            page.Paragraphs.Add(graph);

            // ----- Alternative approach using Page.AddGraphics -----
            // If you need to enforce clipping, you can add graphic elements directly
            // via Page.AddGraphics and supply the clipping rectangle.
            // Note: Graph objects are not GraphicElements, so they cannot be added this way.
            // Instead, you would create GraphicElement instances (e.g., Path) and add them.
            // The following line demonstrates the method signature; actual graphic elements
            // would need to be created accordingly.
            // page.AddGraphics(graphicElementsCollection, clippingRect);
            // Since creating a GraphicElementCollection is not supported in the current API,
            // the above call is left as a placeholder to illustrate the intended usage.

            // Save the PDF document
            doc.Save("output.pdf");
        }
    }
}