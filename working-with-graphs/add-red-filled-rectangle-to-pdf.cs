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

            // Create a Graph container (acts as a canvas for vector shapes)
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(500.0, 500.0);

            // Define a rectangle with absolute coordinates (float parameters are required)
            // left = 100, bottom = 200, width = 150, height = 100
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                100f,   // llx (left)
                200f,   // lly (bottom)
                150f,   // width
                100f);  // height

            // Set visual properties via GraphInfo (FillColor, Stroke Color, LineWidth)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Red,   // solid red fill
                Color = Aspose.Pdf.Color.Black,    // border color (optional)
                LineWidth = 1f                      // border thickness (float literal)
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF – guard the call on non‑Windows platforms where GDI+ (libgdiplus) may be missing
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with red rectangle saved as '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform. " +
                                  "Saving the PDF may require libgdiplus. " +
                                  "Skipping doc.Save() to avoid a TypeInitializationException.");
            }
        }
    }
}
