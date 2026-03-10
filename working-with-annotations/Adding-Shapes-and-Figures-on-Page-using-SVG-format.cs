using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputSvg = "output.svg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Graph constructor now expects double values (float overload is obsolete)
                Graph graph = new Graph(500.0, 400.0);

                // Use the drawing Rectangle (fully qualified) to avoid ambiguity with Aspose.Pdf.Rectangle
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 300, 200, 150);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2
                };
                graph.Shapes.Add(rect);

                // Add an ellipse shape
                Ellipse ellipse = new Ellipse(300, 200, 150, 100);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.Yellow,
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(ellipse);

                // Add a line shape
                float[] linePoints = { 100, 100, 400, 350 };
                Line line = new Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,
                    LineWidth = 2
                };
                graph.Shapes.Add(line);

                // Place the graph on the page
                page.Paragraphs.Add(graph);

                // Save the modified document as SVG
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(outputSvg, svgOptions);
            }

            Console.WriteLine($"SVG file saved to '{outputSvg}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
