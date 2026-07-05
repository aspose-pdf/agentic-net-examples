using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Retrieve the page dimensions (width and height in points)
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Instantiate a Graph object that matches the page size
            Graph graph = new Graph(pageWidth, pageHeight);

            // Example: add a rectangle shape to the graph (optional)
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle) and float parameters
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                100f,                                 // left (X)
                (float)(pageHeight - 200),            // bottom (Y)
                200f,                                 // width
                100f);                                // height

            // Configure visual appearance via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with Graph created successfully.");
    }
}
