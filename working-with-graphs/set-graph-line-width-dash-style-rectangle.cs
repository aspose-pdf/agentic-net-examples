using System;
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

            // Create a Graph container (width: 400pt, height: 200pt) using the double constructor
            Graph graph = new Graph(400.0, 200.0);

            // Configure the Graph's line width and dash style (applies to shapes added later)
            graph.GraphInfo.LineWidth = 2f;               // 2 points
            graph.GraphInfo.DashArray = new int[] { 5, 2 }; // 5pt dash, 2pt gap

            // Create a rectangle shape (left, bottom, width, height)
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                50f,   // left
                100f,  // bottom
                150f,  // width
                80f);  // height

            // Set visual properties for the rectangle, matching the Graph's dash style
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.DarkBlue,
                LineWidth = 2f,
                DashArray = new int[] { 5, 2 } // same dash pattern as the Graph
            };

            // Add the rectangle to the Graph
            graph.Shapes.Add(rect);

            // Add the Graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save("custom_border.pdf");
            }
            else
            {
                try
                {
                    doc.Save("custom_border.pdf");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("PDF creation routine finished.");
    }

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
