using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height)
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(400, 200);

            // Define a rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 300, 100);

            // Set fill color, border color, border width, and dash pattern via GraphInfo
            rect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,   // Fill the rectangle
                Color = Aspose.Pdf.Color.Black,          // Border color
                LineWidth = 2,                           // Border width
                DashArray = new int[] { 5, 3 }           // Dash pattern: 5 points dash, 3 points gap
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}