using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (acts as a paragraph) with specified width and height
            // Use the double overload of the constructor (the float overload is obsolete)
            Graph graph = new Graph(400.0, 300.0); // width = 400 points, height = 300 points

            // Position the graph on the page (optional)
            graph.Left = 50;   // distance from the left edge of the page
            graph.Top  = 500;  // distance from the bottom edge of the page

            // ----- First shape (rectangle) -----
            // Fully qualify the drawing rectangle to avoid ambiguity with Aspose.Pdf.Rectangle
            var rect1 = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 150f, 100f);
            rect1.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect1);

            // ----- Second shape (rectangle) that overlaps the first one -----
            var rect2 = new Aspose.Pdf.Drawing.Rectangle(80f, 40f, 150f, 100f); // Overlaps rect1
            rect2.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect2);

            // NOTE: In recent Aspose.Pdf versions the BoundsCheckMode enum and the
            // UpdateBoundsCheckMode method have been removed. If you need to prevent
            // overlapping shapes you must implement your own logic before adding the
            // shapes to the graph.

            // Add the graph (with its shapes) to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("OverlappingShapes.pdf");
        }

        Console.WriteLine("PDF with graph saved as 'OverlappingShapes.pdf'.");
    }
}