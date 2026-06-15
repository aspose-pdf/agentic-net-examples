using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;
using Aspose.Pdf.Text;

namespace AsposePdfExamples
{
    class GraphClippingRegion
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file (self‑contained example)
            using (Document createDoc = new Document())
            {
                createDoc.Pages.Add();
                createDoc.Save("input.pdf");
            }

            // Open the sample PDF and apply clipping to a graph
            using (Document doc = new Document("input.pdf"))
            {
                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Define clipping rectangle (position and size)
                float clipX = 100f;          // left
                float clipY = 500f;          // bottom
                float clipWidth = 200f;
                float clipHeight = 150f;

                // Build clipping path operators
                OperatorCollection operators = page.Contents;
                operators.Add(new MoveTo(clipX, clipY));
                operators.Add(new LineTo(clipX + clipWidth, clipY));
                operators.Add(new LineTo(clipX + clipWidth, clipY + clipHeight));
                operators.Add(new LineTo(clipX, clipY + clipHeight));
                operators.Add(new ClosePath());
                operators.Add(new Clip()); // Apply clipping using non‑zero winding rule

                // Create a graph larger than the clipping area
                Graph graph = new Graph(400.0, 300.0);

                // Large rectangle covering the whole graph
                Aspose.Pdf.Drawing.Rectangle bigRect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 400f, 300f);
                bigRect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;
                graph.Shapes.Add(bigRect);

                // Ellipse that also exceeds the clipping bounds
                Ellipse ellipse = new Ellipse(50f, 50f, 350f, 250f);
                ellipse.GraphInfo.FillColor = Aspose.Pdf.Color.Blue;
                graph.Shapes.Add(ellipse);

                // Add the graph to the page – it will be rendered only inside the clipping rectangle
                page.Paragraphs.Add(graph);

                // End the clipping region so subsequent content is not clipped
                operators.Add(new EOClip());

                // Save the result
                doc.Save("output.pdf");
            }
        }
    }
}
