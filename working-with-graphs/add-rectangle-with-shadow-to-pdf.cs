using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_with_shadow.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Rectangle coordinates
            double llx = 100; // lower‑left X
            double lly = 500; // lower‑left Y
            double urx = 300; // upper‑right X
            double ury = 600; // upper‑right Y

            // Offset for the shadow rectangle
            double offsetX = 5;
            double offsetY = -5;

            // Create a Graph that spans the whole page – Graph is the only object that can be added to page.Paragraphs
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // ---- Shadow rectangle (semi‑transparent) ----
            var shadow = new Aspose.Pdf.Drawing.Rectangle(
                (float)(llx + offsetX),
                (float)(lly + offsetY),
                (float)(urx + offsetX),
                (float)(ury + offsetY));
            // Use an ARGB colour to express transparency (alpha = 128 → 50 % opacity)
            shadow.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 128, 128, 128); // semi‑transparent gray
            // Optional: no visible border for the shadow
            shadow.GraphInfo.Color = Aspose.Pdf.Color.FromArgb(0, 0, 0, 0);
            graph.Shapes.Add(shadow);

            // ---- Main rectangle (opaque) ----
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                (float)llx,
                (float)lly,
                (float)urx,
                (float)ury);
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.Blue; // fully opaque fill
            rect.GraphInfo.Color = Aspose.Pdf.Color.Black;   // optional border colour
            graph.Shapes.Add(rect);

            // Add the graph (which now contains both rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
