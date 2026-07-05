using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a Graph container (width: 500, height: 400)
            Graph graph = new Graph(500, 400);

            // Draw X axis
            float[] xAxis = { 50, 350, 450, 350 };
            Line xLine = new Line(xAxis);
            xLine.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1 };
            graph.Shapes.Add(xLine);

            // Draw Y axis
            float[] yAxis = { 50, 350, 50, 50 };
            Line yLine = new Line(yAxis);
            yLine.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1 };
            graph.Shapes.Add(yLine);

            // Draw a simple line graph (three segments)
            float[] segment1 = { 50, 350, 150, 250 };
            Line line1 = new Line(segment1);
            line1.GraphInfo = new GraphInfo { Color = Color.Blue, LineWidth = 2 };
            graph.Shapes.Add(line1);

            float[] segment2 = { 150, 250, 250, 300 };
            Line line2 = new Line(segment2);
            line2.GraphInfo = new GraphInfo { Color = Color.Blue, LineWidth = 2 };
            graph.Shapes.Add(line2);

            float[] segment3 = { 250, 300, 350, 200 };
            Line line3 = new Line(segment3);
            line3.GraphInfo = new GraphInfo { Color = Color.Blue, LineWidth = 2 };
            graph.Shapes.Add(line3);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Serialize the PDF to a byte array for network transmission
            using (MemoryStream ms = new MemoryStream())
            {
                pdfDoc.Save(ms);
                byte[] pdfBytes = ms.ToArray();

                // Placeholder: use pdfBytes as needed (e.g., send over network)
                Console.WriteLine($"PDF byte array length: {pdfBytes.Length}");
            }
        }
    }
}