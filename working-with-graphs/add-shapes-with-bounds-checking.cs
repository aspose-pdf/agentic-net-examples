using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "shapes.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page – all drawing shapes must be placed inside a Graph.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            var shapes = new[]
            {
                new { X = 50.0,  Y = 750.0, Width = 200.0, Height = 100.0 }, // valid
                new { X = -10.0, Y = 500.0, Width = 100.0, Height = 100.0 }, // left out of bounds
                new { X = 400.0, Y = 900.0, Width = 300.0, Height = 200.0 }  // top out of bounds
            };

            foreach (var s in shapes)
            {
                try
                {
                    // Rectangle constructor expects float arguments – cast from double.
                    Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                        (float)s.X, (float)s.Y, (float)s.Width, (float)s.Height);

                    // Styling is done through GraphInfo.
                    rect.GraphInfo.Color = Aspose.Pdf.Color.Black;
                    rect.GraphInfo.LineWidth = 2f;

                    // Add the shape to the Graph; the Graph will later be added to the page.
                    graph.Shapes.Add(rect);
                }
                catch (Exception ex)
                {
                    // Log coordinates that caused the exception.
                    Console.WriteLine($"Shape out of bounds: X={s.X}, Y={s.Y}, Width={s.Width}, Height={s.Height}. Exception: {ex.Message}");
                }
            }

            // Add the Graph (containing all successfully created shapes) to the page.
            page.Paragraphs.Add(graph);

            // Save the document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
