using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // needed for TextFragment and FontStyles

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Add a title for the graph
            // -----------------------------------------------------------------
            TextFragment title = new TextFragment("Continuous Data Series");
            // Position the title above the graph area (X = 50, Y = 770)
            title.Position = new Position(50, 770);
            title.TextState.FontSize = 14;
            // Correct enum qualification – FontStyles lives in Aspose.Pdf.Text
            title.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
            page.Paragraphs.Add(title);

            // -----------------------------------------------------------------
            // Create a Graph container. Use width/height constructor (double).
            // The graph will be placed at (50, 500) with a size of 500x250 points.
            // -----------------------------------------------------------------
            double graphWidth = 500;   // matches the rectangle width in the original code
            double graphHeight = 250;  // matches the rectangle height in the original code
            Graph graph = new Graph(graphWidth, graphHeight);

            // Optionally add a border around the graph area
            graph.GraphInfo.Color = Aspose.Pdf.Color.Black;
            graph.GraphInfo.LineWidth = 1;

            // Sample data points for the series (X and Y coordinates)
            double[] xValues = { 0, 50, 100, 150, 200, 250, 300 };
            double[] yValues = { 0, 80, 30, 120, 60, 150, 90 };

            // Add line segments between consecutive points, assigning a color per segment
            for (int i = 0; i < xValues.Length - 1; i++)
            {
                // Define start and end points of the current segment (float array required by Line)
                float[] points = new float[]
                {
                    (float)xValues[i], (float)yValues[i],
                    (float)xValues[i + 1], (float)yValues[i + 1]
                };

                // Choose a color based on the segment index (example: rotating palette)
                Aspose.Pdf.Color segmentColor;
                switch (i % 3)
                {
                    case 0: segmentColor = Aspose.Pdf.Color.Red;   break;
                    case 1: segmentColor = Aspose.Pdf.Color.Green; break;
                    default: segmentColor = Aspose.Pdf.Color.Blue; break;
                }

                // Create the line shape and style it via GraphInfo
                Line line = new Line(points);
                line.GraphInfo = new GraphInfo
                {
                    Color = segmentColor,
                    LineWidth = 2
                };

                // Add the line to the graph
                graph.Shapes.Add(line);
            }

            // Insert the graph into the page's content
            page.Paragraphs.Add(graph);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph PDF saved to '{outputPath}'.");
    }
}
