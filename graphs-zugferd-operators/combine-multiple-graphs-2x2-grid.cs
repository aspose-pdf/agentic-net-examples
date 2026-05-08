using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // TextFragment is needed for graph titles

class Program
{
    static void Main()
    {
        const string outputPath = "combined_graphs.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the 2x2 grid of graphs
            Page page = doc.Pages.Add();

            // Define dimensions for each graph (width x height in points)
            double graphWidth = 250;
            double graphHeight = 200;

            // Layout parameters
            double margin = 50;          // outer margin from page edges
            double spacingX = 20;        // horizontal gap between columns
            double spacingY = 20;        // vertical gap between rows (kept to avoid unused‑variable warning)

            // Calculate Y positions (origin is bottom‑left)
            double yTopRow = page.PageInfo.Height - margin - graphHeight; // top row
            double yBottomRow = margin;                                 // bottom row

            // Calculate X positions for the two columns
            double xCol1 = margin;
            double xCol2 = margin + graphWidth + spacingX;

            // ---------- Graph 1 (top‑left) ----------
            Graph graph1 = new Graph(graphWidth, graphHeight);
            graph1.Left = (float)xCol1;
            graph1.Top = (float)yTopRow;
            graph1.Title = new TextFragment("Graph 1");
            graph1.GraphInfo = new GraphInfo { Color = Color.Blue, LineWidth = 1f };

            // Add a rectangle shape to Graph 1
            var rect1 = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)graphWidth,
                (float)graphHeight);
            rect1.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 0.5f
            };
            graph1.Shapes.Add(rect1);

            // ---------- Graph 2 (top‑right) ----------
            Graph graph2 = new Graph(graphWidth, graphHeight);
            graph2.Left = (float)xCol2;
            graph2.Top = (float)yTopRow;
            graph2.Title = new TextFragment("Graph 2");
            graph2.GraphInfo = new GraphInfo { Color = Color.Green, LineWidth = 1f };

            // Add a horizontal line shape to Graph 2
            float[] linePoints = {
                0f,
                (float)(graphHeight / 2),
                (float)graphWidth,
                (float)(graphHeight / 2)
            };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo { Color = Color.Red, LineWidth = 2f };
            graph2.Shapes.Add(line);

            // ---------- Graph 3 (bottom‑left) ----------
            Graph graph3 = new Graph(graphWidth, graphHeight);
            graph3.Left = (float)xCol1;
            graph3.Top = (float)yBottomRow;
            graph3.Title = new TextFragment("Graph 3");
            graph3.GraphInfo = new GraphInfo { Color = Color.Orange, LineWidth = 1f };

            // Add an ellipse shape to Graph 3
            Ellipse ellipse = new Ellipse(
                (float)(graphWidth / 4),
                (float)(graphHeight / 4),
                (float)(graphWidth / 2),
                (float)(graphHeight / 2));
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.DarkBlue,
                LineWidth = 1f
            };
            graph3.Shapes.Add(ellipse);

            // ---------- Graph 4 (bottom‑right) ----------
            Graph graph4 = new Graph(graphWidth, graphHeight);
            graph4.Left = (float)xCol2;
            graph4.Top = (float)yBottomRow;
            graph4.Title = new TextFragment("Graph 4");
            graph4.GraphInfo = new GraphInfo { Color = Color.Purple, LineWidth = 1f };

            // Add two crossing lines to form an "X"
            float[] line1Points = { 0f, 0f, (float)graphWidth, (float)graphHeight };
            Line line1 = new Line(line1Points);
            line1.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1f };
            graph4.Shapes.Add(line1);

            float[] line2Points = { 0f, (float)graphHeight, (float)graphWidth, 0f };
            Line line2 = new Line(line2Points);
            line2.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1f };
            graph4.Shapes.Add(line2);

            // Add all graphs to the page's paragraph collection
            page.Paragraphs.Add(graph1);
            page.Paragraphs.Add(graph2);
            page.Paragraphs.Add(graph3);
            page.Paragraphs.Add(graph4);

            // Save the resulting PDF – guard against missing libgdiplus on non‑Windows platforms
            SaveDocument(doc, outputPath);
        }

        Console.WriteLine($"PDF with combined graphs saved to '{outputPath}'.");
    }

    private static void SaveDocument(Document doc, string path)
    {
        // On Windows the native GDI+ library is always present.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            return;
        }

        // On macOS / Linux we may not have libgdiplus. Attempt to save and handle the failure gracefully.
        try
        {
            doc.Save(path);
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                              "The PDF was saved without rendering the Graph objects.");
            // Optionally, you could remove the Graph objects and retry saving a plain PDF.
        }
    }

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
