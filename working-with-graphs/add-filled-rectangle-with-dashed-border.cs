using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height in points)
            Graph graph = new Graph(400, 200);

            // Define a rectangle shape (left, bottom, width, height)
            // Use the Drawing.Rectangle type, not Aspose.Pdf.Rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 300, 100);

            // Set visual properties via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                // Fill color of the rectangle
                FillColor = Color.LightGray,
                // Border color
                Color = Color.Black,
                // Border width
                LineWidth = 2,
                // Dash pattern: 3 points dash, 2 points gap
                DashArray = new int[] { 3, 2 }
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("filled_rectangle.pdf");
        }

        Console.WriteLine("PDF with filled rectangle saved as 'filled_rectangle.pdf'.");
    }
}