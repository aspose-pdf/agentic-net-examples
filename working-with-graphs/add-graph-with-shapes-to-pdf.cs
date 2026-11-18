using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_graph.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            Page newPage = doc.Pages.Add();

            // Optionally set the page size (here using A4 dimensions)
            newPage.PageInfo.Width = 595; // points
            newPage.PageInfo.Height = 842; // points

            // Create a Graph object – only width and height are supplied to the constructor
            Graph graph = new Graph(500f, 500f);
            // Position the graph on the page (optional – adjust as needed)
            graph.Left = 50f;
            graph.Top = 300f;

            // Draw a black horizontal line
            Line line = new Line(new float[] { 50f, 50f, 450f, 50f });
            line.GraphInfo.Color = Color.Black;
            line.GraphInfo.LineWidth = 2f;
            graph.Shapes.Add(line);

            // Draw a blue ellipse
            Ellipse ellipse = new Ellipse(100.0, 100.0, 200.0, 200.0);
            ellipse.GraphInfo.Color = Color.Blue;
            ellipse.GraphInfo.FillColor = Color.LightBlue;
            ellipse.GraphInfo.LineWidth = 1.5f;
            graph.Shapes.Add(ellipse);

            // Draw a red rectangle (use fully‑qualified type to avoid ambiguity)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(250f, 250f, 100f, 100f);
            rect.GraphInfo.Color = Color.Red;
            rect.GraphInfo.FillColor = Color.LightCoral;
            rect.GraphInfo.LineWidth = 2f;
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            newPage.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new page and graph: {outputPath}");
    }
}
