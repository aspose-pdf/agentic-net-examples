using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – size in points
            Graph graph = new Graph(500, 300);

            // Example data points (x, y) for a continuous series
            // We'll connect each consecutive pair with a line segment
            float[] dataX = { 50, 150, 250, 350, 450 };
            float[] dataY = { 250, 100, 200, 80, 220 };

            // Define colors for each segment (varying)
            Aspose.Pdf.Color[] segmentColors = {
                Aspose.Pdf.Color.Red,
                Aspose.Pdf.Color.Green,
                Aspose.Pdf.Color.Blue,
                Aspose.Pdf.Color.Orange
            };

            // Add line segments between consecutive points
            for (int i = 0; i < dataX.Length - 1; i++)
            {
                // Position array: {x1, y1, x2, y2}
                float[] linePos = {
                    dataX[i], dataY[i],
                    dataX[i + 1], dataY[i + 1]
                };

                // Create the line shape
                Line line = new Line(linePos);

                // Set visual properties via GraphInfo
                line.GraphInfo = new GraphInfo
                {
                    Color = segmentColors[i % segmentColors.Length],
                    LineWidth = 2
                };

                // Add the line to the graph
                graph.Shapes.Add(line);
            }

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF (inside the using block to ensure proper disposal)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}