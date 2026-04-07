using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace LineGraphExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page to the document
                Page page = doc.Pages.Add();

                // Create a graph canvas (width: 500, height: 400)
                Graph graph = new Graph(500.0, 400.0);
                page.Paragraphs.Add(graph);

                // ---------- Series 1 – Solid line ----------
                float[] pointsSeries1 = new float[] { 50f, 350f, 450f, 350f };
                Line lineSeries1 = new Line(pointsSeries1);
                lineSeries1.GraphInfo = new GraphInfo();
                lineSeries1.GraphInfo.Color = Color.Blue;
                lineSeries1.GraphInfo.LineWidth = 2f;
                // No DashArray – solid line
                graph.Shapes.Add(lineSeries1);

                // ---------- Series 2 – Dashed line (5 on, 5 off) ----------
                float[] pointsSeries2 = new float[] { 50f, 300f, 450f, 300f };
                Line lineSeries2 = new Line(pointsSeries2);
                lineSeries2.GraphInfo = new GraphInfo();
                lineSeries2.GraphInfo.Color = Color.Green;
                lineSeries2.GraphInfo.LineWidth = 2f;
                lineSeries2.GraphInfo.DashArray = new int[] { 5, 5 };
                graph.Shapes.Add(lineSeries2);

                // ---------- Series 3 – Custom dash pattern (2 on, 2 off, 6 on, 2 off) ----------
                float[] pointsSeries3 = new float[] { 50f, 250f, 450f, 250f };
                Line lineSeries3 = new Line(pointsSeries3);
                lineSeries3.GraphInfo = new GraphInfo();
                lineSeries3.GraphInfo.Color = Color.Red;
                lineSeries3.GraphInfo.LineWidth = 2f;
                lineSeries3.GraphInfo.DashArray = new int[] { 2, 2, 6, 2 };
                graph.Shapes.Add(lineSeries3);

                // Save the PDF – guard against missing GDI+ on non‑Windows platforms
                string outputPath = "line-graph.pdf";
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
                        Console.WriteLine($"PDF saved to '{outputPath}'.");
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                    }
                }
            }
        }

        private static bool ContainsDllNotFound(Exception ex)
        {
            Exception current = ex;
            while (current != null)
            {
                if (current is DllNotFoundException)
                {
                    return true;
                }
                current = current.InnerException;
            }
            return false;
        }
    }
}
