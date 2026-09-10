using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "dimension_rectangle.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container with desired size (e.g., 400x200 points)
            // Use double parameters as the float overload is obsolete
            Graph graph = new Graph(400.0, 200.0);

            // Define rectangle dimensions: left=50, bottom=50, width=200, height=100
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 200f, 100f);

            // Set visual properties via GraphInfo (optional)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,   // Fill color
                Color = Aspose.Pdf.Color.Black,           // Border color
                LineWidth = 2f                             // Border thickness
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dimension‑specific rectangle saved to '{outputPath}'.");
    }
}
