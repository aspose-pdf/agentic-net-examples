using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string backgroundPath = "background.jpg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(backgroundPath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Apply the background image to all pages using PdfFileStamp.
            // ------------------------------------------------------------
            // PdfFileStamp works with the Facades API; it can add a stamp that
            // is rendered behind the page content (IsBackground = true).
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(doc);                     // initialize with the document

            // Fully qualify the Stamp type to avoid ambiguity with Aspose.Pdf.Stamp.
            Aspose.Pdf.Facades.Stamp bgStamp = new Aspose.Pdf.Facades.Stamp();
            bgStamp.BindImage(backgroundPath);          // use the image as stamp content
            bgStamp.IsBackground = true;               // render it as page background

            // Size the stamp to cover the first page (all pages have the same size in most PDFs).
            // MediaBox provides the page dimensions in points.
            Page firstPage = doc.Pages[1];
            float pageWidth  = (float)firstPage.MediaBox.Width;
            float pageHeight = (float)firstPage.MediaBox.Height;
            bgStamp.SetImageSize(pageWidth, pageHeight);
            bgStamp.SetOrigin(0f, 0f);                  // lower‑left corner at (0,0)

            fileStamp.AddStamp(bgStamp);                // add the background stamp
            fileStamp.Close();                          // apply the stamp to the document

            // ------------------------------------------------------------
            // 2. Create a Graph and draw shapes on top of the background.
            // ------------------------------------------------------------
            // The Graph is a paragraph that can contain vector shapes.
            // Setting ZIndex > 0 ensures the graph is rendered above the background stamp.
            Graph graph = new Graph(600.0, 800.0)        // width, height (double)
            {
                ZIndex = 1                               // draw over the background
            };

            // ---- Rectangle shape ----
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                100f,               // left
                600f,               // bottom
                200f,               // width
                100f);              // height
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ---- Ellipse shape ----
            var ellipse = new Aspose.Pdf.Drawing.Ellipse(
                350f,               // left
                600f,               // bottom
                150f,               // width
                100f);              // height
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color     = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ---- Line shape ----
            float[] linePoints = { 100f, 500f, 300f, 500f }; // x1, y1, x2, y2
            var line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the graph to the first page (after the background stamp).
            firstPage.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // 3. Save the modified PDF.
            // ------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with background image and overlaid graph saved to '{outputPdfPath}'.");
    }
}
