using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string outputPdf = "graph_with_background.pdf";
        const string bgImagePath = "background.png";

        if (!File.Exists(bgImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {bgImagePath}");
            return;
        }

        // Load an existing PDF or create a new one
        using (Document doc = File.Exists(inputPdf) ? new Document(inputPdf) : new Document())
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Add the background image (drawn first → appears behind later content)
            // ------------------------------------------------------------
            using (FileStream imgStream = File.OpenRead(bgImagePath))
            {
                // left, bottom, right, top (points)
                Aspose.Pdf.Rectangle imgRect = new Aspose.Pdf.Rectangle(50, 400, 550, 800);
                page.AddImage(imgStream, imgRect);
            }

            // ------------------------------------------------------------
            // 2. Create a Graph that will hold vector shapes.
            //    Use the double‑parameter constructor (the float overload is obsolete).
            // ------------------------------------------------------------
            Graph graph = new Graph(500.0, 300.0) // width, height in points (double literals)
            {
                Left = 50.0,   // X position on the page
                Top = 400.0,   // Y position on the page (same as image for overlay)
                ZIndex = 1     // Higher ZIndex draws on top of earlier page content
            };

            // ------------------------------------------------------------
            // 3. Add a filled rectangle shape to the graph.
            //    Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle).
            // ------------------------------------------------------------
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectShape);

            // ------------------------------------------------------------
            // 4. Add a line shape on top of the rectangle.
            // ------------------------------------------------------------
            float[] linePoints = { 0f, 0f, 300f, 150f };
            var lineShape = new Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2f
            };
            graph.Shapes.Add(lineShape);

            // ------------------------------------------------------------
            // 5. Add the graph to the page – it will be rendered over the image.
            // ------------------------------------------------------------
            page.Paragraphs.Add(graph);

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
