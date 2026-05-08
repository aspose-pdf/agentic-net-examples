using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Determine if we are running on Windows (GDI+ is available)
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                // Create a Graph container (width: 400 points, height: 200 points) – double literals as required
                Graph graph = new Graph(400.0, 200.0);

                // Define a rectangle shape that will serve as the background of the graph
                var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 400f, 200f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray, // background fill
                    Color = Color.Black,         // border color
                    LineWidth = 1f               // border thickness (float)
                };

                // Add the rectangle to the graph's shape collection
                graph.Shapes.Add(rect);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Skipping Graph rendering on non‑Windows platform (libgdiplus is required).");
            }

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello Aspose.Pdf!");

            // Set font family, size, and color via the TextState object
            tf.TextState.Font = FontRepository.FindFont("Helvetica"); // font family
            tf.TextState.FontSize = 24;                               // font size
            tf.TextState.ForegroundColor = Color.Blue;                // font color

            // Position the text inside the graph (coordinates are from the bottom‑left of the page)
            tf.Position = new Position(100, 150); // X = 100, Y = 150

            // Add the text fragment to the page (it will be rendered on top of the graph if the graph exists)
            page.Paragraphs.Add(tf);

            // Save the PDF document – guard the call on non‑Windows platforms where GDI+ may be missing
            string outputPath = "output.pdf";
            if (isWindows)
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (graph omitted).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a missing native GDI+ library
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
