using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "combined_graphs.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Page dimensions (default A4: 595 x 842 points)
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Each graph occupies half the page width and height
            double graphWidth = pageWidth / 2;
            double graphHeight = pageHeight / 2;

            // Top‑left graph
            Graph graph1 = CreateSampleGraph(graphWidth, graphHeight);
            graph1.Left = 0;
            graph1.Top = pageHeight - graphHeight; // origin is bottom‑left
            page.Paragraphs.Add(graph1);

            // Top‑right graph
            Graph graph2 = CreateSampleGraph(graphWidth, graphHeight);
            graph2.Left = graphWidth;
            graph2.Top = pageHeight - graphHeight;
            page.Paragraphs.Add(graph2);

            // Bottom‑left graph
            Graph graph3 = CreateSampleGraph(graphWidth, graphHeight);
            graph3.Left = 0;
            graph3.Top = 0;
            page.Paragraphs.Add(graph3);

            // Bottom‑right graph
            Graph graph4 = CreateSampleGraph(graphWidth, graphHeight);
            graph4.Left = graphWidth;
            graph4.Top = 0;
            page.Paragraphs.Add(graph4);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with combined graphs saved to '{outputPath}'.");
    }

    // Creates a simple graph containing a filled rectangle and a diagonal line
    static Graph CreateSampleGraph(double width, double height)
    {
        // Initialize the graph container (Graph constructor expects double)
        Graph graph = new Graph(width, height);

        // Rectangle covering the entire graph area – Rectangle constructor expects float values
        var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, (float)width, (float)height);
        rect.GraphInfo = new GraphInfo
        {
            FillColor = Aspose.Pdf.Color.LightGray,
            Color = Aspose.Pdf.Color.Black,
            LineWidth = 1f // float literal
        };
        graph.Shapes.Add(rect);

        // Diagonal line from bottom‑left to top‑right – Line constructor expects a float[]
        float[] linePoints = { 0f, 0f, (float)width, (float)height };
        var line = new Aspose.Pdf.Drawing.Line(linePoints);
        line.GraphInfo = new GraphInfo
        {
            Color = Aspose.Pdf.Color.Red,
            LineWidth = 2f // float literal
        };
        graph.Shapes.Add(line);

        return graph;
    }
}
