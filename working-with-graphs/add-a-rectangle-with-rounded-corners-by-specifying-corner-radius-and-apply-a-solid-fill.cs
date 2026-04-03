using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rounded_rectangle.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph that covers the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define rectangle position and size (left, bottom, width, height)
            float left   = 100f;
            float bottom = 500f;
            float width  = 200f;
            float height = 100f;

            // Create the rectangle shape with rounded corners
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(left, bottom, width, height)
            {
                RoundedCornerRadius = 15f, // corner radius
                GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray, // solid fill color
                    Color     = Color.Black,    // border color
                    LineWidth = 1f
                }
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document (guard against missing GDI+ on non‑Windows platforms)
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
                catch (TypeInitializationException)
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; the PDF was not saved.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}