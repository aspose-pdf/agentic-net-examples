using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ArcGraph.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Add the Graph only on platforms that have GDI+ (Windows).
            // On non‑Windows platforms the Graph object would trigger a TypeInitializationException
            // because libgdiplus is missing.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Graph size in points (double literals as required by the constructor)
                Graph graph = new Graph(400.0, 300.0);

                // Define an arc (centerX, centerY, radius, startAngle, sweepAngle)
                Arc arc = new Arc(200f, 150f, 100f, 0f, 180f)
                {
                    GraphInfo = new GraphInfo
                    {
                        // Light‑blue fill color
                        FillColor = Color.FromRgb(0.2, 0.5, 0.8),
                        // Stroke color and width
                        Color = Color.Black,
                        LineWidth = 1f
                    }
                };

                graph.Shapes.Add(arc);
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Non‑Windows platform detected – Graph rendering requires GDI+. " +
                                  "Saving PDF without the arc.");
            }

            // Save the PDF. On Windows we have the full graph; on other platforms we saved a page
            // without any drawing objects, which does not need GDI+.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with filled arc saved to '{outputPath}'.");
            }
            else
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF (without graph) saved to '{outputPath}'.");
            }
        }
    }
}
