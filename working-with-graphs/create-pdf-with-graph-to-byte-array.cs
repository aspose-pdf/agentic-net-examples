using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // <-- added for TextFragment

class PdfGraphSerializer
{
    public static byte[] CreatePdfWithGraph()
    {
        using (MemoryStream output = new MemoryStream())
        {
            using (Document doc = new Document())
            {
                // Add a new page (1‑based indexing)
                Page page = doc.Pages.Add();

                // On Windows we can render the graph (requires GDI+). On other platforms we skip it to avoid libgdiplus errors.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Use the Graph constructor that accepts double values (as required by newer Aspose.Pdf versions)
                    Graph graph = new Graph(400.0, 300.0);

                    // Draw X‑axis
                    float[] xAxisPoints = { 50f, 250f, 350f, 250f };
                    Line xAxis = new Line(xAxisPoints)
                    {
                        GraphInfo = new GraphInfo
                        {
                            Color = Color.Black,
                            LineWidth = 2f
                        }
                    };
                    graph.Shapes.Add(xAxis);

                    // Draw Y‑axis
                    float[] yAxisPoints = { 50f, 250f, 50f, 50f };
                    Line yAxis = new Line(yAxisPoints)
                    {
                        GraphInfo = new GraphInfo
                        {
                            Color = Color.Black,
                            LineWidth = 2f
                        }
                    };
                    graph.Shapes.Add(yAxis);

                    // Draw a simple data line (example chart)
                    float[] dataPoints = { 50f, 250f, 100f, 200f, 150f, 150f, 200f, 180f, 250f, 120f };
                    Line dataLine = new Line(dataPoints)
                    {
                        GraphInfo = new GraphInfo
                        {
                            Color = Color.Blue,
                            LineWidth = 2f
                        }
                    };
                    graph.Shapes.Add(dataLine);

                    // Add the graph to the page
                    page.Paragraphs.Add(graph);
                }
                else
                {
                    // Optional: add a placeholder text indicating that graph rendering is unavailable on this platform.
                    page.Paragraphs.Add(new TextFragment("Graph rendering requires GDI+ (libgdiplus) which is unavailable on this OS."));
                }

                // Serialize PDF to the memory stream – guard against missing GDI+ on non‑Windows platforms.
                try
                {
                    doc.Save(output);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    // Fallback: return an empty PDF (already created) without the graph.
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without graph rendering.");
                }
            }

            // Return the PDF as a byte array for transmission
            return output.ToArray();
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

    // Entry point required for a console application / build
    static void Main()
    {
        byte[] pdfBytes = CreatePdfWithGraph();
        Console.WriteLine($"PDF generated, size: {pdfBytes.Length} bytes");
        // Uncomment the line below to write the PDF to disk for manual verification
        // File.WriteAllBytes("graph.pdf", pdfBytes);
    }
}