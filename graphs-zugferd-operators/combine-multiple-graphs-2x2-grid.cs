using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "combined_graphs.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page (default size A4)
            Page page = doc.Pages.Add();

            // Define grid layout parameters
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            double margin = 20; // margin around the page
            double spacing = 10; // spacing between graphs

            // Calculate cell size for a 2x2 grid
            double cellWidth = (pageWidth - 2 * margin - spacing) / 2;
            double cellHeight = (pageHeight - 2 * margin - spacing) / 2;

            // Helper to create a sample graph
            Graph CreateSampleGraph(double width, double height, Aspose.Pdf.Color fillColor, Aspose.Pdf.Color strokeColor)
            {
                Graph graph = new Graph(width, height);

                // Example rectangle shape (drawing rectangle, not page rectangle)
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)width,
                    (float)height);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = fillColor,
                    Color = strokeColor,
                    LineWidth = 1
                };
                graph.Shapes.Add(rect);

                // Example diagonal line
                float[] linePoints = { 0f, 0f, (float)width, (float)height };
                Line line = new Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 0.5f
                };
                graph.Shapes.Add(line);

                return graph;
            }

            // Create four graphs with different colors
            Graph graph1 = CreateSampleGraph(cellWidth, cellHeight, Aspose.Pdf.Color.LightGray, Aspose.Pdf.Color.DarkGray);
            Graph graph2 = CreateSampleGraph(cellWidth, cellHeight, Aspose.Pdf.Color.LightBlue, Aspose.Pdf.Color.Blue);
            Graph graph3 = CreateSampleGraph(cellWidth, cellHeight, Aspose.Pdf.Color.LightGreen, Aspose.Pdf.Color.Green);
            Graph graph4 = CreateSampleGraph(cellWidth, cellHeight, Aspose.Pdf.Color.LightCoral, Aspose.Pdf.Color.Red);

            // Position graphs in a 2x2 grid
            // Top‑left
            graph1.Left = margin;
            graph1.Top = margin;
            // Top‑right
            graph2.Left = margin + cellWidth + spacing;
            graph2.Top = margin;
            // Bottom‑left
            graph3.Left = margin;
            graph3.Top = margin + cellHeight + spacing;
            // Bottom‑right
            graph4.Left = margin + cellWidth + spacing;
            graph4.Top = margin + cellHeight + spacing;

            // Add graphs to the page
            page.Paragraphs.Add(graph1);
            page.Paragraphs.Add(graph2);
            page.Paragraphs.Add(graph3);
            page.Paragraphs.Add(graph4);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with combined graphs saved to '{outputPath}'.");
    }
}
