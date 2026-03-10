using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_alpha.pdf";

        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // On non‑Windows platforms libgdiplus may be missing – Graph rendering depends on System.Drawing.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.WriteLine("Graph rendering requires libgdiplus which is not available. Saving PDF without rectangle.");
                doc.Save(outputPath);
                return;
            }

            // Create a Graph container – use double parameters (float overload is obsolete)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
            rect.GraphInfo = new GraphInfo
            {
                // 50 % transparent red (alpha = 128 out of 255)
                FillColor = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0),
                Color = Aspose.Pdf.Color.Black, // border color
                LineWidth = 2
            };

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Optional: flatten transparency so the alpha channel is rasterized
            doc.FlattenTransparency();

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with semi‑transparent rectangle saved to '{outputPath}'.");
    }
}
