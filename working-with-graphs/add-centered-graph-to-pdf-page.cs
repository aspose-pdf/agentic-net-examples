using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; get the first page
            Page page = doc.Pages[1];

            // Create a Graph (container for shapes) with desired size and center it on the page
            Graph graph = new Graph(400.0, 300.0)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Optional title for the graph
            graph.Title = new TextFragment("Sample Bar Chart");

            // Sample data for a simple bar chart
            double[] values = { 10, 35, 20, 45, 30 };
            float barWidth = 40f;
            float spacing = 20f;
            float startX = 30f;
            float baseline = 250f; // Y coordinate of the X‑axis

            for (int i = 0; i < values.Length; i++)
            {
                float height = (float)values[i] * 4; // simple scaling factor
                float x = startX + i * (barWidth + spacing);
                float y = baseline - height;

                // Create a rectangle shape that represents a bar
                Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(x, y, barWidth, height);
                bar.GraphInfo.FillColor = Color.SteelBlue;   // fill colour of the bar
                bar.GraphInfo.Color = Color.Black;          // stroke colour
                bar.GraphInfo.LineWidth = 1f;

                // Add a label under the bar
                bar.Text = new TextFragment($"Q{i + 1}");

                graph.Shapes.Add(bar);
            }

            // Add the graph to the page's Paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph added and saved to '{outputPath}'.");
    }
}
