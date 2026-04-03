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
        const string outputPath = "report.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define graph dimensions (width, height) in points
            double graphWidth = 500;
            double graphHeight = 300;

            // Create the graph container
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                Left = 50,   // position from the left edge of the page
                Top  = 400   // position from the bottom edge of the page
            };

            // Sample data categories
            var categories = new[]
            {
                new { Label = "Category A", Value = 40, Color = Color.FromRgb(0.2, 0.6, 0.8) },
                new { Label = "Category B", Value = 30, Color = Color.FromRgb(0.8, 0.3, 0.2) },
                new { Label = "Category C", Value = 20, Color = Color.FromRgb(0.4, 0.8, 0.4) },
                new { Label = "Category D", Value = 10, Color = Color.FromRgb(0.9, 0.7, 0.2) }
            };

            // Layout parameters for the bars
            float barWidth = 40;
            float spacing  = 20;
            float startX   = 20; // X offset inside the graph
            float baseY    = 20; // Y offset (bottom margin) inside the graph

            // Draw a rectangle for each category
            for (int i = 0; i < categories.Length; i++)
            {
                var cat = categories[i];
                float rectX = startX + i * (barWidth + spacing);
                float rectHeight = (float)(cat.Value * 2); // simple scaling factor

                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                    rectX,
                    baseY,
                    barWidth,
                    rectHeight);

                rect.GraphInfo = new GraphInfo
                {
                    FillColor = cat.Color,
                    Color     = Color.Black,
                    LineWidth = 1f
                };

                graph.Shapes.Add(rect);
            }

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Add a legend (colored boxes + labels)
            float legendX = 300;
            float legendY = 350;

            for (int i = 0; i < categories.Length; i++)
            {
                var cat = categories[i];

                // Small colored box
                Aspose.Pdf.Drawing.Rectangle box = new Aspose.Pdf.Drawing.Rectangle(
                    0,
                    0,
                    10,
                    10);
                box.GraphInfo = new GraphInfo
                {
                    FillColor = cat.Color,
                    Color     = Color.Black,
                    LineWidth = 0.5f
                };
                Graph boxGraph = new Graph(10, 10)
                {
                    Left = legendX,
                    Top  = legendY - i * 20
                };
                boxGraph.Shapes.Add(box);
                page.Paragraphs.Add(boxGraph);

                // Corresponding text label
                TextFragment tf = new TextFragment(cat.Label)
                {
                    Position = new Position(legendX + 15, legendY - i * 20 + 2)
                };
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Color.Black;
                page.Paragraphs.Add(tf);
            }

            // Save the document – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("GDI+ (libgdiplus) not available; PDF saved without graphical rendering.");
                }
            }
        }

        Console.WriteLine($"Report generated: {outputPath}");
    }

    // Helper to detect nested DllNotFoundException
    private static bool ContainsDllNotFound(Exception ex)
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