using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file using the dedicated load options.
        CgmLoadOptions loadOptions = new CgmLoadOptions(); // default A4 300dpi page size
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Ensure there is at least one page.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were loaded from the CGM file.");
                return;
            }

            // Work with the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // Add vector shapes using a Graph container.
            // ------------------------------------------------------------
            // Create a Graph of arbitrary size (width, height) – it will be
            // positioned by the shapes themselves, so the size can be large.
            Graph graph = new Graph(500f, 500f);

            // Aspose.Pdf.Drawing.Rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100f, 600f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2.0f
            };
            graph.Shapes.Add(rectShape);

            // Ellipse shape (left, bottom, width, height)
            Ellipse ellipseShape = new Ellipse(350f, 600f, 150f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipseShape);

            // Add the Graph to the page.
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // Add a figure annotation (square) as an example of a "figure".
            // ------------------------------------------------------------
            // Define the rectangle that bounds the annotation.
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100f, 500f, 300f, 550f);
            SquareAnnotation square = new SquareAnnotation(page, annotRect)
            {
                Color = Color.Blue,               // border color
                Contents = "Sample square figure",
                Title = "Figure 1"
            };
            page.Annotations.Add(square);

            // Save the resulting PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with added shapes saved to '{outputPdf}'.");
    }
}
