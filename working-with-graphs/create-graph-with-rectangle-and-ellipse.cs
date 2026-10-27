using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (size 500x500). Use the double overload as the float overload is obsolete.
            Graph graph = new Graph(500.0, 500.0);

            // ----- Rectangle with solid fill (red) -----
            var rect = new Aspose.Pdf.Drawing.Rectangle(50, 400, 200, 250);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromRgb(1, 0, 0) // solid red
            };
            graph.Shapes.Add(rect);

            // ----- Ellipse with solid fill (blue) -----
            var ellipse = new Ellipse(300, 300, 450, 450);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromRgb(0, 0, 1) // solid blue
            };
            graph.Shapes.Add(ellipse);

            // ----- Second rectangle with solid fill (green) -----
            var rect2 = new Aspose.Pdf.Drawing.Rectangle(250, 50, 450, 150);
            rect2.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromRgb(0, 1, 0) // solid green
            };
            graph.Shapes.Add(rect2);

            // Add the completed graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
