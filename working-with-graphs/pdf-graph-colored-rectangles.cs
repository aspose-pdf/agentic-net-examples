using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Load the sample PDF and add a graph with colored rectangles and a legend
        using (Document doc = new Document("input.pdf"))
        {
            Page page = doc.Pages[1]; // 1‑based indexing

            // Create a graph (width, height)
            Graph graph = new Graph(500.0, 300.0);
            graph.Title = new TextFragment("Sales by Category");
            graph.Left = 50f;
            graph.Top = 400f;

            // Category A – red rectangle
            Aspose.Pdf.Drawing.Rectangle rectA = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rectA.GraphInfo = new GraphInfo { FillColor = Color.Red, Color = Color.Black, LineWidth = 1f };
            graph.Shapes.Add(rectA);

            // Category B – green rectangle
            Aspose.Pdf.Drawing.Rectangle rectB = new Aspose.Pdf.Drawing.Rectangle(0f, 60f, 100f, 50f);
            rectB.GraphInfo = new GraphInfo { FillColor = Color.Green, Color = Color.Black, LineWidth = 1f };
            graph.Shapes.Add(rectB);

            // Category C – blue rectangle
            Aspose.Pdf.Drawing.Rectangle rectC = new Aspose.Pdf.Drawing.Rectangle(0f, 120f, 100f, 50f);
            rectC.GraphInfo = new GraphInfo { FillColor = Color.Blue, Color = Color.Black, LineWidth = 1f };
            graph.Shapes.Add(rectC);

            // Legend text fragments
            TextFragment legendA = new TextFragment("Category A");
            legendA.Position = new Position(110f, 25f);
            page.Paragraphs.Add(legendA);

            TextFragment legendB = new TextFragment("Category B");
            legendB.Position = new Position(110f, 85f);
            page.Paragraphs.Add(legendB);

            TextFragment legendC = new TextFragment("Category C");
            legendC.Position = new Position(110f, 145f);
            page.Paragraphs.Add(legendC);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the final PDF report
            doc.Save("output.pdf");
        }
    }
}
