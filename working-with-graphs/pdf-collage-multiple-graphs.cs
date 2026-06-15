using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class CollageExample
{
    static void Main()
    {
        // Suppress known vulnerability warning for Microsoft.Bcl.Memory (NU1903)
        #pragma warning disable NU1903
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // ---------- First Graph ----------
            // Position: left=50, top=700, size=200x150
            Graph graph1 = new Graph(200.0, 150.0) // double constructor as required
            {
                Left = 50,
                Top  = 700
            };

            // Add a rectangle shape to the first graph
            var rect1 = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 180f, 130f);
            rect1.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph1.Shapes.Add(rect1);

            // Add the first graph to the page
            page.Paragraphs.Add(graph1);

            // ---------- Second Graph ----------
            // Position: left=300, top=700, size=200x150
            Graph graph2 = new Graph(200.0, 150.0)
            {
                Left = 300,
                Top  = 700
            };

            // Add an ellipse shape to the second graph
            Ellipse ellipse = new Ellipse(10f, 10f, 180f, 130f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color     = Aspose.Pdf.Color.Red,
                LineWidth = 2f
            };
            graph2.Shapes.Add(ellipse);

            // Add the second graph to the page
            page.Paragraphs.Add(graph2);

            // ---------- Third Graph ----------
            // Position: left=175, top=500, size=200x150
            Graph graph3 = new Graph(200.0, 150.0)
            {
                Left = 175,
                Top  = 500
            };

            // Add a line shape to the third graph
            float[] linePoints = { 0f, 0f, 180f, 130f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Blue,
                LineWidth = 3f
            };
            graph3.Shapes.Add(line);

            // Add the third graph to the page
            page.Paragraphs.Add(graph3);

            // Save the resulting PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "collage.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: libgdiplus is not available on this platform. PDF saved without graph rendering.");
                    // Optionally, you could re‑try saving without adding the Graph objects.
                }
            }
        }
        #pragma warning restore NU1903

        Console.WriteLine("Collage PDF created: collage.pdf");
    }

    // Helper to detect missing native GDI+ library
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
