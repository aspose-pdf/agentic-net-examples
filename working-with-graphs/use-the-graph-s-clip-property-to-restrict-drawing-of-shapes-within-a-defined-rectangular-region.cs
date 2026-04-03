using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the clipping rectangle (left, bottom, width, height)
            // Rectangle constructor expects float values
            var clipRect = new Aspose.Pdf.Drawing.Rectangle(
                100f,          // left (X)
                500f,          // bottom (Y)
                200f,          // width
                150f);         // height
            // Optional visual styling – will not be visible after clipping
            clipRect.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Transparent,
                FillColor = Aspose.Pdf.Color.Transparent
            };

            // Create a Graph container – use double literals as required by the new constructor
            Graph graph = new Graph(400.0, 300.0);

            // Add the clipping rectangle to the graph's shape collection
            graph.Shapes.Add(clipRect);

            // Apply the clipping path – add a Clip operator before the shapes that must be clipped
            graph.Operators.Add(new Clip());

            // Add a shape that extends beyond the clipping rectangle
            var bigRect = new Aspose.Pdf.Drawing.Rectangle(
                50f,   // left
                450f,  // bottom
                300f,  // width
                200f); // height
            bigRect.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Black,
                FillColor = Aspose.Pdf.Color.LightGray,
                LineWidth = 2f
            };
            graph.Shapes.Add(bigRect);

            // End the clipping region
            graph.Operators.Add(new EndClip());

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("ClippedShapes.pdf");
        }

        Console.WriteLine("PDF created with shapes clipped to the defined rectangle.");
    }
}
