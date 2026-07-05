using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class ExportVectorGraph
{
    static void Main()
    {
        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a blank page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a Graph container (width: 400 points, height: 200 points)
            // NOTE: Graph constructor expects double values
            Graph graph = new Graph(400.0, 200.0);

            // ----- Example shapes -----

            // Rectangle (left: 0, bottom: 0, width: 200, height: 100)
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle) and float literals
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Ellipse (left: 220, bottom: 0, width: 150, height: 100)
            var ellipse = new Ellipse(220f, 0f, 150f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // Line (from (0,150) to (350,150))
            float[] linePoints = { 0f, 150f, 350f, 150f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 3f
            };
            graph.Shapes.Add(line);

            // Add the Graph (vector graphic) to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF with the embedded vector graphic
            pdfDoc.Save("VectorGraph.pdf");
        }

        Console.WriteLine("PDF with embedded vector graphic saved as 'VectorGraph.pdf'.");
    }
}