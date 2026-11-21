using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;
using DrawRect = Aspose.Pdf.Drawing.Rectangle; // drawing rectangle (shape)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all paragraphs on the page
                foreach (var paragraph in page.Paragraphs)
                {
                    // Graph objects are added as paragraphs; identify them via type check
                    if (paragraph is Graph graph)
                    {
                        // Iterate through each shape contained in the graph
                        foreach (var shape in graph.Shapes)
                        {
                            // Determine the bounding box based on the concrete shape type
                            var bbox = GetBoundingBox(shape);

                            // If we could compute a box, log it; otherwise just log the type
                            if (bbox != null)
                            {
                                Console.WriteLine(
                                    $"Page {pageIndex}, Shape {shape.GetType().Name}: " +
                                    $"LLX={bbox.LLX}, LLY={bbox.LLY}, URX={bbox.URX}, URY={bbox.URY}");
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"Page {pageIndex}, Shape {shape.GetType().Name}: (no bounding box available)");
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Computes a bounding box for the supplied shape. Returns null when the shape type does not expose
    /// enough geometric information to build a box (e.g., a simple Line).
    /// </summary>
    private static Aspose.Pdf.Rectangle GetBoundingBox(Shape shape)
    {
        switch (shape)
        {
            case DrawRect r:
                // Left, Bottom, Width, Height are double values – cast to float for the Rectangle ctor
                return new Aspose.Pdf.Rectangle(
                    (float)r.Left,
                    (float)r.Bottom,
                    (float)(r.Left + r.Width),
                    (float)(r.Bottom + r.Height));

            case Ellipse e:
                return new Aspose.Pdf.Rectangle(
                    (float)e.Left,
                    (float)e.Bottom,
                    (float)(e.Left + e.Width),
                    (float)(e.Bottom + e.Height));

            case Circle c:
                // Circle is defined by centre (PosX,PosY) and Radius (double)
                float leftC = (float)(c.PosX - c.Radius);
                float bottomC = (float)(c.PosY - c.Radius);
                float rightC = (float)(c.PosX + c.Radius);
                float topC = (float)(c.PosY + c.Radius);
                return new Aspose.Pdf.Rectangle(leftC, bottomC, rightC, topC);

            case Arc a:
                // Arc shares the same centre/radius definition as Circle – use the same calculation
                float leftA = (float)(a.PosX - a.Radius);
                float bottomA = (float)(a.PosY - a.Radius);
                float rightA = (float)(a.PosX + a.Radius);
                float topA = (float)(a.PosY + a.Radius);
                return new Aspose.Pdf.Rectangle(leftA, bottomA, rightA, topA);

            case Line l:
                // Line stores its points as a float[]; compute min/max of X/Y values.
                // The property exposing the raw points is "Points" (available via the internal field).
                // Since there is no public accessor, we fall back to "no bounding box" for Line.
                return null;

            default:
                // Other shape types (Curve, Path, etc.) are not covered here.
                return null;
        }
    }
}
