using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 200 points, height: 100 points)
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(200.0, 100.0);

            // Define a rectangle shape (left, bottom, width, height)
            // Rectangle constructor expects float values
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                (float)0,   // left
                (float)0,   // bottom
                (float)150, // width
                (float)80   // height
            );

            // Set corner radius for rounded corners
            rect.RoundedCornerRadius = 10; // radius in points

            // Set fill and stroke properties using GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue,
                Color = Aspose.Pdf.Color.Black, // stroke color
                LineWidth = 1f                    // float literal as required
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Position the graph on the page (optional: set top‑left coordinates)
            graph.Left = 50;   // distance from the left edge of the page
            graph.Top  = 600;  // distance from the bottom edge of the page

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "RectangleGraph.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was created but may lack graphical elements that require GDI+.");
                }
            }
        }

        Console.WriteLine("PDF with rectangle graph saved as 'RectangleGraph.pdf'.");
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
