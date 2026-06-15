using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace AddShadowRectangleGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file (self‑contained example)
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Open the sample PDF and add a graph with a rectangle and its shadow
            using (Document doc = new Document("input.pdf"))
            {
                Page page = doc.Pages[1]; // 1‑based indexing

                // Create a graph canvas
                Graph graph = new Graph(400.0, 300.0);
                graph.Left = 0.0f;
                graph.Top = 0.0f;
                page.Paragraphs.Add(graph);

                // Define rectangle size and position
                float rectX = 100.0f;
                float rectY = 100.0f;
                float rectWidth = 200.0f;
                float rectHeight = 100.0f;

                // Create shadow rectangle (offset and semi‑transparent)
                Aspose.Pdf.Drawing.Rectangle shadowRect = new Aspose.Pdf.Drawing.Rectangle(rectX + 5.0f, rectY - 5.0f, rectWidth, rectHeight);
                shadowRect.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 0); // 50% transparent black
                graph.Shapes.Add(shadowRect);

                // Create main filled rectangle
                Aspose.Pdf.Drawing.Rectangle mainRect = new Aspose.Pdf.Drawing.Rectangle(rectX, rectY, rectWidth, rectHeight);
                mainRect.GraphInfo.FillColor = Aspose.Pdf.Color.Blue;
                graph.Shapes.Add(mainRect);

                // Save the result
                doc.Save("output.pdf");
            }
        }
    }
}
