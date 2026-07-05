using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Use a using block for deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Shape coordinates that deliberately exceed the page bounds
            // Default A4 page size is approximately 595 x 842 points
            double left   = 500;   // X coordinate of the lower‑left corner
            double bottom = 800;   // Y coordinate of the lower‑left corner
            double width  = 200;   // Width that will overflow the page width
            double height = 100;   // Height that will overflow the page height

            // Aspose.Pdf.Drawing.Rectangle expects *float* arguments – cast the doubles
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                (float)left,
                (float)bottom,
                (float)width,
                (float)height);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                // LineWidth is a float as well
                LineWidth = 1f
            };

            // Perform manual bounds validation and throw the library‑specific exception if needed
            try
            {
                double containerWidth  = page.PageInfo.Width;
                double containerHeight = page.PageInfo.Height;

                bool fits = left >= 0 && bottom >= 0 &&
                            (left + width) <= containerWidth &&
                            (bottom + height) <= containerHeight;

                if (!fits)
                {
                    throw new BoundsOutOfRangeException("Shape does not fit within the page bounds.");
                }

                Console.WriteLine("Shape fits within the page bounds.");
            }
            catch (BoundsOutOfRangeException ex)
            {
                // Log detailed information about the shape and the container
                Console.WriteLine("BoundsOutOfRangeException caught:");
                Console.WriteLine($"Message          : {ex.Message}");
                Console.WriteLine($"Shape type       : {rect.GetType().Name}");
                Console.WriteLine($"Left             : {left}");
                Console.WriteLine($"Bottom           : {bottom}");
                Console.WriteLine($"Width            : {width}");
                Console.WriteLine($"Height           : {height}");
                Console.WriteLine($"Container Width  : {page.PageInfo.Width}");
                Console.WriteLine($"Container Height : {page.PageInfo.Height}");
            }

            // Wrap the rectangle in a Graph (Graph accepts double dimensions) and add it to the page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the PDF inside the using block
            doc.Save("output.pdf");
        }
    }
}
