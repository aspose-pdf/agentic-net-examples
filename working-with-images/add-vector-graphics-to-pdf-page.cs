using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "result.pdf";

        // Load an existing PDF if it exists, otherwise create a new document
        using (Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document())
        {
            // Add a blank page at the end of the document
            Page newPage = doc.Pages.Add();

            // Create a Graph container – the constructor now expects double values
            Graph graph = new Graph(500.0, 700.0);

            // ---------- Rectangle shape ----------
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 600f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ---------- Ellipse shape ----------
            var ellipse = new Ellipse(300f, 500f, 150f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ---------- Line shape ----------
            float[] linePoints = { 100f, 400f, 400f, 400f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the Graph (which holds all shapes) to the page
            newPage.Paragraphs.Add(graph);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
