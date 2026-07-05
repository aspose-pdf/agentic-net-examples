using System;
using System.IO;
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

            // Create a Graph container (size 400x200 points)
            Graph graph = new Graph(400, 200);

            // Define a rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 300, 100);

            // Set visual properties via GraphInfo:
            //   - LineWidth = 2 points (border thickness)
            //   - Color = Black (border color)
            //   - DashArray = {3,3} (dashed line style)
            rect.GraphInfo = new GraphInfo
            {
                LineWidth = 2,
                Color = Aspose.Pdf.Color.Black,
                DashArray = new int[] { 3, 3 }
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with dashed rectangle saved as 'output.pdf'.");
    }
}