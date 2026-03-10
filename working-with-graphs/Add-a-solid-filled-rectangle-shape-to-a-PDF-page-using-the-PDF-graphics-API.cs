using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "solid_rectangle.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // On macOS the Graph class requires native GDI+ (libgdiplus).
            // If the library is not present we skip the Graph rendering to avoid a TypeInitializationException.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Console.WriteLine("libgdiplus is required for Graph rendering on macOS; saving PDF without rectangle.");
                doc.Save(outputPath);
                return;
            }

            // Graph container – use double literals as required by the constructor
            Graph graph = new Graph(500.0, 500.0);

            // Drawing rectangle – fully‑qualified type and float literals (required by the constructor)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100.0F, 400.0F, 200.0F, 100.0F);

            // Styling via GraphInfo (fill, border color and line width)
            rect.GraphInfo.FillColor = Color.LightGray; // solid fill
            rect.GraphInfo.Color = Color.Black;        // border color
            rect.GraphInfo.LineWidth = 2;               // border thickness

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with solid‑filled rectangle saved to '{outputPath}'.");
    }
}
