using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "LayeredGraphics.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Create two optional‑content groups (layers)
            // -------------------------------------------------
            Layer backgroundLayer = new Layer("Background", "bg");
            Layer foregroundLayer = new Layer("Foreground", "fg");
            page.Layers.Add(backgroundLayer);
            page.Layers.Add(foregroundLayer);

            // -------------------------------------------------
            // Draw a semi‑transparent rectangle on the background layer
            // -------------------------------------------------
            Graph bgGraph = new Graph(500.0, 400.0);
            Aspose.Pdf.Drawing.Rectangle bgRect = new Aspose.Pdf.Drawing.Rectangle(50.0F, 200.0F, 300.0F, 150.0F);
            bgRect.GraphInfo = new GraphInfo
            {
                // 50 % transparent light‑blue fill (alpha = 128)
                FillColor = Aspose.Pdf.Color.FromArgb(128, 51, 153, 255),
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2.0F
            };
            bgGraph.Shapes.Add(bgRect);
            // Optional: associate the graph with the background layer (if the API version supports LayerId)
            // bgGraph.GraphInfo.LayerId = backgroundLayer.Id;
            page.Paragraphs.Add(bgGraph);

            // -------------------------------------------------
            // Draw a semi‑transparent ellipse on the foreground layer
            // -------------------------------------------------
            Graph fgGraph = new Graph(500.0, 400.0);
            Aspose.Pdf.Drawing.Ellipse fgEllipse = new Aspose.Pdf.Drawing.Ellipse(200.0F, 250.0F, 250.0F, 150.0F);
            fgEllipse.GraphInfo = new GraphInfo
            {
                // 40 % transparent light‑red fill (alpha = 102)
                FillColor = Aspose.Pdf.Color.FromArgb(102, 230, 76, 76),
                Color = Aspose.Pdf.Color.DarkRed,
                LineWidth = 2.0F
            };
            fgGraph.Shapes.Add(fgEllipse);
            // fgGraph.GraphInfo.LayerId = foregroundLayer.Id; // optional
            page.Paragraphs.Add(fgGraph);

            // -------------------------------------------------
            // Draw a semi‑transparent line that crosses both shapes
            // -------------------------------------------------
            Graph lineGraph = new Graph(500.0, 400.0);
            float[] linePoints = { 0.0F, 0.0F, 500.0F, 400.0F };
            Aspose.Pdf.Drawing.Line crossingLine = new Aspose.Pdf.Drawing.Line(linePoints);
            crossingLine.GraphInfo = new GraphInfo
            {
                // 60 % transparent green (alpha = 153)
                Color = Aspose.Pdf.Color.FromArgb(153, 0, 255, 0),
                LineWidth = 5.0F
            };
            lineGraph.Shapes.Add(crossingLine);
            page.Paragraphs.Add(lineGraph);

            // -------------------------------------------------
            // Save the PDF document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with layered graphics saved to '{outputPath}'.");
    }
}
