using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a PDF document that will be disposed automatically
        using (var pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Create a Graph container
            // ------------------------------------------------------------
            // The Graph class only has constructors that accept width/height.
            // Position is set via the Left and Top properties after construction.
            var graph = new Graph(500, 300) // width = 500 pt, height = 300 pt
            {
                Left = 50,               // distance from the left edge of the page
                Top = 750,               // distance from the bottom edge of the page
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black),
                Title = new TextFragment("Sample Line Graph") // Title uses TextFragment, not Caption
            };

            // ------------------------------------------------------------
            // 2. Draw a simple line series inside the graph
            // ------------------------------------------------------------
            // Sample data points (X,Y) are expressed in graph coordinates.
            // Here we manually map five sample values to a poly‑line.
            float[] linePoints = new float[]
            {
                0,   0,   // point 0
                100, 50,  // point 1
                200, 30,  // point 2
                300, 80,  // point 3
                400, 60   // point 4
            };
            var line = new Line(linePoints)
            {
                GraphInfo = { Color = Color.Blue, LineWidth = 2f }
            };
            graph.Shapes.Add(line);

            // ------------------------------------------------------------
            // 3. (Optional) Draw X and Y axes for visual reference
            // ------------------------------------------------------------
            var xAxis = new Line(new float[] { 0, 0, 500, 0 })
            {
                GraphInfo = { Color = Color.Black, LineWidth = 1f }
            };
            var yAxis = new Line(new float[] { 0, 0, 0, 300 })
            {
                GraphInfo = { Color = Color.Black, LineWidth = 1f }
            };
            graph.Shapes.Add(xAxis);
            graph.Shapes.Add(yAxis);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // 4. Serialize the PDF to a byte array for network transmission
            // ------------------------------------------------------------
            byte[] pdfBytes;
            using (var ms = new MemoryStream())
            {
                pdfDoc.Save(ms);
                pdfBytes = ms.ToArray();
            }

            Console.WriteLine($"PDF byte array length: {pdfBytes.Length}");
        }
    }
}
