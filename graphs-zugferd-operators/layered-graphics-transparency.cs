using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample PDF with a single blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Reopen the PDF and add graphics with transparency
        using (Document doc = new Document("input.pdf"))
        {
            Page page = doc.Pages[1];

            // Create a Graph container to hold drawing shapes
            Graph graph = new Graph(600.0, 800.0); // width, height of the graph

            // Add a semi‑transparent red rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100.0F, 500.0F, 200.0F, 200.0F);
            GraphInfo rectInfo = new GraphInfo();
            rectInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0); // 50% transparent red
            rect.GraphInfo = rectInfo;
            graph.Shapes.Add(rect);

            // Add a semi‑transparent blue ellipse
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(200.0F, 400.0F, 200.0F, 200.0F);
            GraphInfo ellipseInfo = new GraphInfo();
            ellipseInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255); // 50% transparent blue
            ellipse.GraphInfo = ellipseInfo;
            graph.Shapes.Add(ellipse);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the result
            doc.Save("output.pdf");
        }
    }
}