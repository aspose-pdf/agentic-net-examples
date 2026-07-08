using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "skewed_graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height in points)
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(400, 200);

            // Apply a skew transformation on the X axis (degrees)
            graph.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                SkewAngleX = 30 // slant the graph 30 degrees on the X axis
            };

            // Create a rectangle shape to demonstrate the skew effect
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            rect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}