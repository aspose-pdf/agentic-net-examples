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

            // Create a Graph container (width: 400, height: 200)
            Graph graph = new Graph(400, 200);

            // Define a rectangle shape (left, bottom, width, height)
            // Use Aspose.Pdf.Drawing.Rectangle for the shape
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 50, 300, 150);

            // Set corner radius
            rectShape.RoundedCornerRadius = 20; // radius in points

            // Set solid fill color via GraphInfo
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue,
                Color = Aspose.Pdf.Color.Black, // optional stroke color
                LineWidth = 1
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rectShape);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with rounded rectangle saved as 'output.pdf'.");
    }
}