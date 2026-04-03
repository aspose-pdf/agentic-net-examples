using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (float overload is obsolete)
            Graph graph = new Graph(500.0, 400.0);

            // Create an Arc shape:
            //   posX, posY – center of the arc
            //   radius      – radius of the arc
            //   alpha       – start angle (degrees)
            //   beta        – end angle (degrees)
            Arc arc = new Arc(250f, 200f, 100f, 0f, 180f);

            // Set the fill color of the arc (custom red color)
            arc.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(255, 0, 0);

            // Add the arc to the graph's shape collection
            graph.Shapes.Add(arc);

            // Add the graph (which now contains the filled arc) to the page
            page.Paragraphs.Add(graph);

            string outputPath = "filled_arc.pdf";

            // Guard Document.Save on platforms that lack GDI+ (e.g., macOS/Linux without libgdiplus)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping doc.Save() because GDI+ (libgdiplus) is not available on this platform.");
                // Optionally, you could attempt to save and catch the TypeInitializationException here.
            }
        }

        Console.WriteLine("Program finished.");
    }
}
