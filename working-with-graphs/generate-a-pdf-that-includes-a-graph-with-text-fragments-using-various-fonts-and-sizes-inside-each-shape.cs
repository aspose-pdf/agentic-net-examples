using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_text.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 500 points, height: 400 points)
            // NOTE: Graph constructor expects double values
            Graph graph = new Graph(500.0, 400.0)
            {
                Left = 50.0,   // X position on the page
                Top  = 400.0   // Y position on the page (from bottom)
            };

            // ---------- Rectangle shape (Aspose.Pdf.Drawing.Rectangle) ----------
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rectShape);

            // Text fragment inside the rectangle
            TextFragment tfRect = new TextFragment("Rect Text")
            {
                Position = new Position(20, 60) // Relative to graph origin
            };
            tfRect.TextState.Font = FontRepository.FindFont("Helvetica");
            tfRect.TextState.FontSize = 14;
            tfRect.TextState.ForegroundColor = Color.Blue;

            // ---------- Ellipse shape ----------
            Ellipse ellipse = new Ellipse(250f, 0f, 150f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.DarkRed,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // Text fragment inside the ellipse
            TextFragment tfEllipse = new TextFragment("Ellipse")
            {
                Position = new Position(260, 40)
            };
            tfEllipse.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            tfEllipse.TextState.FontSize = 12;
            tfEllipse.TextState.ForegroundColor = Color.Green;

            // ---------- Line shape ----------
            float[] linePoints = { 0f, 150f, 400f, 150f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Color.Red,
                LineWidth = 3f
            };
            graph.Shapes.Add(line);

            // Text fragment near the line
            TextFragment tfLine = new TextFragment("Line Label")
            {
                Position = new Position(200, 160)
            };
            tfLine.TextState.Font = FontRepository.FindFont("Courier");
            tfLine.TextState.FontSize = 10;
            tfLine.TextState.ForegroundColor = Color.Purple;

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Append all text fragments to the page (absolute positioning)
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tfRect);
            builder.AppendText(tfEllipse);
            builder.AppendText(tfLine);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Non‑Windows platform – ensure libgdiplus is installed for full rendering.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
