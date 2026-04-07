using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph container (double parameters are required)
            Graph graph = new Graph(500.0, 400.0);

            // ----- Add a rectangle shape -----
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(
                50f,   // left
                300f,  // bottom
                200f,  // width
                100f   // height
            );
            rectShape.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Black,
                FillColor = Aspose.Pdf.Color.LightGray
            };
            graph.Shapes.Add(rectShape);

            // ----- Add an ellipse shape -----
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(
                300f, // left
                200f, // bottom
                150f, // width
                100f  // height
            );
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue
            };
            graph.Shapes.Add(ellipse);

            // ----- Add a line shape -----
            float[] linePoints = { 100f, 100f, 400f, 200f };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Attach the graph to the page
            page.Paragraphs.Add(graph);

            // ----- Iterate through all shapes and log their bounding boxes -----
            foreach (Shape shape in graph.Shapes)
            {
                switch (shape)
                {
                    case Aspose.Pdf.Drawing.Rectangle r:
                        double llx = r.Left;
                        double lly = r.Bottom;
                        double urx = r.Left + r.Width;
                        double ury = r.Bottom + r.Height;
                        Console.WriteLine($"Rectangle - LLX:{llx}, LLY:{lly}, URX:{urx}, URY:{ury}");
                        break;

                    case Aspose.Pdf.Drawing.Ellipse e:
                        double ellLlx = e.Left;
                        double ellLly = e.Bottom;
                        double ellUrx = e.Left + e.Width;
                        double ellUry = e.Bottom + e.Height;
                        Console.WriteLine($"Ellipse - LLX:{ellLlx}, LLY:{ellLly}, URX:{ellUrx}, URY:{ellUry}");
                        break;

                    case Aspose.Pdf.Drawing.Line l:
                        // Line does not expose a direct bounding box; log that it is a line.
                        Console.WriteLine("Line shape - bounding box not directly available.");
                        break;

                    default:
                        Console.WriteLine($"Shape type {shape.GetType().Name} - bounding box handling not implemented.");
                        break;
                }
            }

            // Save the PDF (output path can be adjusted as needed)
            doc.Save("output.pdf");
        }

        Console.WriteLine("Processing completed.");
    }
}