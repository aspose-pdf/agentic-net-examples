using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "DimensionRectangle.pdf";

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Graph rendering depends on GDI+ (libgdiplus). Guard it on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a Graph container with desired size (width, height in points)
                Graph graph = new Graph(400.0, 300.0); // double literals as required

                // Define a rectangle shape with specific dimensions inside the graph
                var rectShape = new Aspose.Pdf.Drawing.Rectangle(
                    100f,   // left
                    150f,   // bottom
                    200f,   // width
                    100f);  // height

                // Set visual properties via GraphInfo (drawing‑rectangle‑properties rule)
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray, // interior fill
                    Color = Aspose.Pdf.Color.Black,         // border color
                    LineWidth = 2f                           // border thickness (float literal)
                };

                // Optionally, set a text label inside the rectangle using TextFragment
                rectShape.Text = new TextFragment("Dimension‑Specific Rectangle");

                // Add the rectangle shape to the graph's shape collection
                graph.Shapes.Add(rectShape);

                // Add the graph to the page's paragraphs collection
                page.Paragraphs.Add(graph);
            }
            else
            {
                // On platforms without libgdiplus, add a placeholder note instead of a Graph.
                page.Paragraphs.Add(new TextFragment(
                    "Graph rendering requires GDI+ (libgdiplus) which is unavailable on this platform."));
            }

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF could not be saved.");
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
