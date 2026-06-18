using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_text.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Page dimensions (needed for positioning)
            double pageHeight = page.PageInfo.Height;

            // Define graph dimensions (width, height) and position (left, bottom)
            double graphWidth  = 400;
            double graphHeight = 200;
            double graphLeft   = 100; // X coordinate of the graph's lower‑left corner
            double graphBottom = 400; // Y coordinate of the graph's lower‑left corner

            // Calculate the Y coordinate of the graph's upper‑left corner (Aspose.Pdf uses Top for vertical placement)
            double graphTop = pageHeight - graphBottom - graphHeight;

            // Create the Graph container using the double‑precision constructor (per verified fix)
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                Left = (float)graphLeft,
                Top  = (float)graphTop,
                GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.LightGray, // border color
                    LineWidth = 1f                     // float literal as required
                }
            };

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ----- Add text inside the graph -----
            // Text to display
            const string text = "Sample Text";

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment(text)
            {
                // Position the text (coordinates are relative to the page origin, bottom‑left)
                // Choose coordinates that lie within the graph rectangle
                Position = new Position(
                    graphLeft + 20,                                 // 20 points inside the graph from left edge
                    graphBottom + graphHeight - 30)                 // 30 points below the top edge of the graph
            };

            // Configure font family, size and color via the existing TextState instance
            tf.TextState.Font = FontRepository.FindFont("Helvetica"); // font family
            tf.TextState.FontSize = 14;                                 // font size
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;      // text color

            // Add the text fragment to the page (it will render over the graph)
            page.Paragraphs.Add(tf);

            // ----- Save the document -----
            // Guard the Save call on macOS/Linux where libgdiplus may be missing (per verified fix)
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated without rendering the graph.");
                }
            }
        }
    }

    // Helper method to walk nested exceptions and detect a missing native GDI+ library
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
