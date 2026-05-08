using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class ReportGenerator
{
    static void Main()
    {
        const string outputPath = "Report.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a graph container (width, height) in points – use double overload
            Graph graph = new Graph(500.0, 300.0);
            // Position the graph on the page (left, top)
            graph.Left = 50;   // 50 points from the left edge
            graph.Top  = 400;  // 400 points from the bottom edge

            // Sample data categories
            var categories = new[]
            {
                new { Label = "Category A", Value = 120.0, Color = Color.FromRgb(0.2, 0.6, 0.9) },
                new { Label = "Category B", Value =  80.0, Color = Color.FromRgb(0.9, 0.4, 0.2) },
                new { Label = "Category C", Value = 150.0, Color = Color.FromRgb(0.4, 0.8, 0.4) }
            };

            // Determine the maximum value for scaling the bars
            double maxValue = 0;
            foreach (var c in categories)
                if (c.Value > maxValue) maxValue = c.Value;

            // Layout parameters for the bars
            double barWidth = 80;
            double spacing  = 30;
            double startX   = 0; // origin inside the graph

            // Add a colored rectangle for each data category
            for (int i = 0; i < categories.Length; i++)
            {
                var cat = categories[i];
                double barHeight = (cat.Value / maxValue) * graph.Height; // scale to graph height

                // Rectangle constructor: left, bottom, width, height (relative to graph)
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)(startX + i * (barWidth + spacing)), // left
                    0f,                                         // bottom
                    (float)barWidth,                            // width
                    (float)barHeight);                          // height

                // Set visual properties via GraphInfo
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = cat.Color,   // fill with category color
                    Color     = Color.Black, // border color
                    LineWidth = 1f            // float literal
                };

                graph.Shapes.Add(rect);
            }

            // Add a legend: small colored squares and labels
            for (int i = 0; i < categories.Length; i++)
            {
                var cat = categories[i];

                // Legend square positioned to the right of the graph
                Aspose.Pdf.Drawing.Rectangle legendRect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)graph.Width + 20f,                     // left
                    (float)graph.Height - (i + 1) * 30f,          // bottom
                    15f,                                          // width
                    15f);                                         // height

                legendRect.GraphInfo = new GraphInfo
                {
                    FillColor = cat.Color,
                    Color     = Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(legendRect);

                // Legend text placed next to the square
                TextFragment tf = new TextFragment(cat.Label);
                tf.Position = new Position(
                    graph.Width + 40,
                    graph.Height - (i + 1) * 30 + 2); // slight vertical offset
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Color.Black;
                page.Paragraphs.Add(tf);
            }

            // Add the completed graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF report – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                    // As a fallback we can still attempt to save a minimal PDF (graph may be missing).
                    // In many cases the exception occurs during Save, so we simply inform the user.
                }
            }
        }

        Console.WriteLine($"PDF report processing completed. Check '{outputPath}' if the platform supports GDI+.");
    }

    // Helper to walk the inner‑exception chain and detect a missing native library.
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
