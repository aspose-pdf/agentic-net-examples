using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample PDF file (self‑contained example)
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the sample PDF and add a rectangle with alpha transparency
        using (Document doc = new Document("input.pdf"))
        {
            Page page = doc.Pages[1];

            // Define rectangle area (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 200f);

            // Set fill color with alpha channel (transparent to opaque effect)
            shapeRect.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255); // 50% transparent blue

            // Create a Graph container larger than the rectangle and add the rectangle
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(600.0, 800.0);
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save("output.pdf");
        }
    }
}