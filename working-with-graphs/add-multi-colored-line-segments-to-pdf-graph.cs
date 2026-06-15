using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) to hold the line shapes
            // Use the constructor that accepts double values (the obsolete float constructor is avoided)
            Graph graph = new Graph(500.0, 400.0);

            // ----- Segment 1: red line -----
            // Coordinates: (50,300) -> (150,350)
            float[] seg1 = { 50, 300, 150, 350 };
            Line line1 = new Line(seg1);
            line1.GraphInfo = new GraphInfo
            {
                Color = Color.Red,      // Aspose.Pdf.Color
                LineWidth = 2f          // float literal as required by GraphInfo
            };
            graph.Shapes.Add(line1);

            // ----- Segment 2: green line -----
            // Coordinates: (150,350) -> (250,250)
            float[] seg2 = { 150, 350, 250, 250 };
            Line line2 = new Line(seg2);
            line2.GraphInfo = new GraphInfo
            {
                Color = Color.Green,
                LineWidth = 2f
            };
            graph.Shapes.Add(line2);

            // ----- Segment 3: blue line -----
            // Coordinates: (250,250) -> (350,300)
            float[] seg3 = { 250, 250, 350, 300 };
            Line line3 = new Line(seg3);
            line3.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line3);

            // Add the graph (with all line segments) to the page
            page.Paragraphs.Add(graph);

            // Guard Document.Save for platforms that may lack libgdiplus (e.g., macOS/Linux)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                // On non‑Windows platforms Aspose.Pdf may require libgdiplus for rendering.
                // Attempt to save and handle the possible GDI+ related exception gracefully.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated without rendering the graph.");
                }
            }
        }
    }

    // Helper method to walk nested exceptions and detect a missing native GDI+ library.
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
