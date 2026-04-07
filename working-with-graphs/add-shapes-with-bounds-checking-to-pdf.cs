using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    // Custom exception for bounds checking
    public class BoundsOutOfRangeException : Exception
    {
        public BoundsOutOfRangeException(string message) : base(message) { }
    }

    static void Main()
    {
        const string outputPath = "shapes.pdf";

        // Use a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a new page
            Page page = doc.Pages.Add();

            // Retrieve page dimensions for bounds checking
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Create a Graph container that covers the whole page (use double constructor as required)
            Graph graph = new Graph(pageWidth, pageHeight);

            // Define shapes (some fit, some exceed page bounds)

            // Aspose.Pdf.Drawing.Rectangle that fits within the page
            var rectFit = new Aspose.Pdf.Drawing.Rectangle(50f, 700f, 150f, 100f);
            rectFit.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };

            // Aspose.Pdf.Drawing.Rectangle that exceeds the page bounds
            var rectOverflow = new Aspose.Pdf.Drawing.Rectangle(500f, 800f, 200f, 100f);
            rectOverflow.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 2f
            };

            // Ellipse that fits within the page
            var ellipseFit = new Ellipse(100f, 100f, 150f, 100f);
            ellipseFit.GraphInfo = new GraphInfo
            {
                FillColor = Color.Green,
                Color = Color.DarkGreen,
                LineWidth = 1f
            };

            // Ellipse that exceeds the page bounds
            var ellipseOverflow = new Ellipse(400f, 900f, 200f, 150f);
            ellipseOverflow.GraphInfo = new GraphInfo
            {
                FillColor = Color.Blue,
                Color = Color.DarkBlue,
                LineWidth = 1f
            };

            // Line that fits within the page
            float[] linePointsFit = { 100f, 400f, 300f, 400f };
            var lineFit = new Line(linePointsFit);
            lineFit.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 2f
            };

            // Line that exceeds the page bounds
            float[] linePointsOverflow = { 500f, 900f, 800f, 950f };
            var lineOverflow = new Line(linePointsOverflow);
            lineOverflow.GraphInfo = new GraphInfo
            {
                Color = Color.Purple,
                LineWidth = 2f
            };

            // Helper method to add a shape with bounds checking
            void AddShape(Shape shape, string name, float[] extraInfo = null)
            {
                try
                {
                    bool fits = true;

                    switch (shape)
                    {
                        case Aspose.Pdf.Drawing.Rectangle r:
                            // Rectangle uses Left, Bottom, Width, Height
                            double rectLLX = r.Left;
                            double rectLLY = r.Bottom;
                            double rectURX = r.Left + r.Width;
                            double rectURY = r.Bottom + r.Height;
                            fits = rectLLX >= 0 && rectLLY >= 0 && rectURX <= pageWidth && rectURY <= pageHeight;
                            break;
                        case Ellipse e:
                            // Ellipse also exposes Left, Bottom, Width, Height
                            double ellLLX = e.Left;
                            double ellLLY = e.Bottom;
                            double ellURX = e.Left + e.Width;
                            double ellURY = e.Bottom + e.Height;
                            fits = ellLLX >= 0 && ellLLY >= 0 && ellURX <= pageWidth && ellURY <= pageHeight;
                            break;
                        case Line l:
                            if (extraInfo != null && extraInfo.Length == 4)
                            {
                                double x1 = extraInfo[0];
                                double y1 = extraInfo[1];
                                double x2 = extraInfo[2];
                                double y2 = extraInfo[3];
                                fits = x1 >= 0 && y1 >= 0 && x2 >= 0 && y2 >= 0 &&
                                       x1 <= pageWidth && y1 <= pageHeight &&
                                       x2 <= pageWidth && y2 <= pageHeight;
                            }
                            else
                            {
                                fits = false; // cannot determine, treat as overflow
                            }
                            break;
                        default:
                            fits = true; // unknown shape, assume it fits
                            break;
                    }

                    if (!fits)
                        throw new BoundsOutOfRangeException($"{name} does not fit within page bounds.");

                    // If the shape fits, add it to the graph
                    graph.Shapes.Add(shape);
                }
                catch (BoundsOutOfRangeException ex)
                {
                    // Log the exception message
                    Console.WriteLine($"Bounds error: {ex.Message}");

                    // Log shape‑specific coordinates
                    switch (shape)
                    {
                        case Aspose.Pdf.Drawing.Rectangle r:
                            Console.WriteLine($"Rectangle coordinates: LLX={r.Left}, LLY={r.Bottom}, URX={r.Left + r.Width}, URY={r.Bottom + r.Height}");
                            break;
                        case Ellipse e:
                            Console.WriteLine($"Ellipse coordinates: LLX={e.Left}, LLY={e.Bottom}, URX={e.Left + e.Width}, URY={e.Bottom + e.Height}");
                            break;
                        case Line l:
                            if (extraInfo != null && extraInfo.Length == 4)
                            {
                                Console.WriteLine($"Line points: ({extraInfo[0]},{extraInfo[1]}) to ({extraInfo[2]},{extraInfo[3]})");
                            }
                            break;
                    }
                }
            }

            // Add shapes, passing line point arrays for logging when needed
            AddShape(rectFit, "RectFit");
            AddShape(rectOverflow, "RectOverflow");
            AddShape(ellipseFit, "EllipseFit");
            AddShape(ellipseOverflow, "EllipseOverflow");
            AddShape(lineFit, "LineFit", linePointsFit);
            AddShape(lineOverflow, "LineOverflow", linePointsOverflow);

            // Attach the graph to the page
            page.Paragraphs.Add(graph);

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
