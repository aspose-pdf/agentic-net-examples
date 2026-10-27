using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists; otherwise create a blank document.
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();
                blank.Save(inputPath);
            }
        }

        // Open the PDF and work within a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Add a new page if the document has none.
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Use the first page for demonstration.
            Page page = doc.Pages[1];

            // Create a Graph container sized to the page.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape (left, bottom, width, height).
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100, 300, 200, 200);

            // Configure the rectangle's border (line width and dash style) via GraphInfo.
            rectShape.GraphInfo.Color = Aspose.Pdf.Color.Blue;          // Border color
            rectShape.GraphInfo.LineWidth = 2.0f;                        // Border thickness
            rectShape.GraphInfo.DashArray = new int[] { 3, 2 };         // 3 units on, 2 units off
            rectShape.GraphInfo.DashPhase = 0;                         // Start of dash pattern
            rectShape.GraphInfo.FillColor = Aspose.Pdf.Color.Transparent; // No fill

            // Add the rectangle to the graph.
            graph.Shapes.Add(rectShape);

            // Add the graph to the page.
            page.Paragraphs.Add(graph);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom rectangle border to '{outputPath}'.");
    }
}
