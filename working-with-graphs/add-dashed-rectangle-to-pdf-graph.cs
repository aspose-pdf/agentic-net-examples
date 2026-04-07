using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400 points, height: 200 points)
            Graph graph = new Graph(400, 200);

            // Define a rectangle shape (left, bottom, width, height)
            // Use the drawing namespace to avoid ambiguity with Aspose.Pdf.Rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);

            // Set visual properties via GraphInfo:
            // - LineWidth = 2 points (border thickness)
            // - Color = Black (border color)
            // - DashArray = {3,3} creates a dashed line (3 points dash, 3 points gap)
            rect.GraphInfo = new GraphInfo
            {
                LineWidth = 2,
                Color = Aspose.Pdf.Color.Black,
                DashArray = new int[] { 3, 3 } // dashed style
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraphs so it is rendered
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with dashed rectangle saved as 'output.pdf'.");
    }
}