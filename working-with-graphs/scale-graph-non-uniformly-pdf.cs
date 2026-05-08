using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_graph.pdf";

        // Create a new PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Initialize a Graph with a base width and height (double values as required)
            Graph graph = new Graph(400.0, 200.0);

            // Configure non‑uniform scaling via GraphInfo
            // ScalingRateX > 1 stretches the X‑axis, ScalingRateY < 1 compresses the Y‑axis
            graph.GraphInfo = new GraphInfo
            {
                ScalingRateX = 1.5, // 150 % scaling on X axis
                ScalingRateY = 0.75 //  75 % scaling on Y axis
            };

            // Add a simple rectangle shape to visualize the scaling effect
            // NOTE: Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Place the graph on the page
            page.Paragraphs.Add(graph);

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform; attempting to save may require libgdiplus.");
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Failed to save PDF: GDI+ (libgdiplus) is not available on this platform.");
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