using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "clipped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            // Define clipping rectangle (lower‑left x, y, upper‑right x, y)
            // Using low‑level PDF operators instead of the missing Operators.Rectangle class
            page.Contents.Add(new MoveTo(100f, 100f));
            page.Contents.Add(new LineTo(300f, 100f));
            page.Contents.Add(new LineTo(300f, 300f));
            page.Contents.Add(new LineTo(100f, 300f));
            page.Contents.Add(new ClosePath());
            page.Contents.Add(new Clip()); // non‑zero winding rule

            // Graph container – use double literals as required by the constructor
            Graph graph = new Graph(400.0, 200.0);

            // Red line that extends beyond the clipping rectangle
            float[] linePoints = { 50f, 50f, 350f, 150f };
            Line line = new Line(linePoints)
            {
                GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 3f
                }
            };
            graph.Shapes.Add(line);

            // Blue rectangle partially outside the clipping area
            var shapeRect = new Aspose.Pdf.Drawing.Rectangle(150f, 150f, 300f, 100f);
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue,
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page – it will be rendered after the Clip operator
            page.Paragraphs.Add(graph);

            // End the clipping region
            page.Contents.Add(new EOClip());

            doc.Save(outputPath);
        }

        Console.WriteLine($"Clipped PDF saved to '{outputPath}'.");
    }
}
