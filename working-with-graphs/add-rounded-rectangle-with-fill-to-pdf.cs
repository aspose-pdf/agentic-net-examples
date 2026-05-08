using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – this holds vector shapes
            // Use double literals as the Graph(float,float) constructor is obsolete
            Graph graph = new Graph(400.0, 200.0); // size in points

            // Define a rectangle shape (left, bottom, width, height)
            // Rectangle constructor expects float values
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 100f);

            // Set the corner radius for rounded corners
            rect.RoundedCornerRadius = 10f; // radius in points

            // Apply a solid fill color and optional border via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue, // solid fill
                Color = Aspose.Pdf.Color.Black,        // border color
                LineWidth = 1f                         // border thickness (float literal)
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph (containing the rectangle) to the page's paragraphs
            page.Paragraphs.Add(graph);

            // Save the resulting PDF to a file – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                // On macOS/Linux Aspose.Pdf may require libgdiplus for rendering Graph objects.
                // Either install libgdiplus or skip the Graph rendering. Here we still attempt to save
                // the document without throwing an unhandled exception.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform)." );
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was created without rendering the Graph shape.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
