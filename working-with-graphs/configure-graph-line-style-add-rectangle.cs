using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt) – use double literals as required by the constructor
            Graph graph = new Graph(400.0, 200.0);

            // Configure the Graph's line width and dash style (these affect shapes that inherit the GraphInfo if not overridden)
            graph.GraphInfo.LineWidth = 2f;               // 2 points
            graph.GraphInfo.DashArray = new int[] { 5, 3 }; // 5pt dash, 3pt gap

            // Create a rectangle shape for the Graph – use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            // The constructor expects float values for left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                50f,   // left
                50f,   // bottom
                250f,  // width  (300 - 50)
                100f   // height (150 - 50)
            );

            // Set visual properties of the rectangle via its GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,   // background fill
                Color = Color.Black,           // border color
                LineWidth = 2f,                // rectangle border width
                DashArray = new int[] { 5, 3 } // same dash pattern as the graph
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraphs
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("custom_border.pdf");
        }

        Console.WriteLine("PDF with custom bordered rectangle saved as 'custom_border.pdf'.");
    }
}
