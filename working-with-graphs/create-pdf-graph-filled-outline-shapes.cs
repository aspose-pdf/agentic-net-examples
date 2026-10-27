using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height)
            Graph graph = new Graph(500, 400);

            // -------------------------------------------------
            // Filled rectangle (stroke + fill)
            // -------------------------------------------------
            // left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle filledRect = new Aspose.Pdf.Drawing.Rectangle(50, 150, 150, 150);
            filledRect.GraphInfo.FillColor = Aspose.Pdf.Color.LightBlue;   // Fill color
            filledRect.GraphInfo.Color = Aspose.Pdf.Color.Black;          // Border color
            filledRect.GraphInfo.LineWidth = 2;                           // Border thickness
            graph.Shapes.Add(filledRect);

            // -------------------------------------------------
            // Unfilled ellipse (stroke only)
            // -------------------------------------------------
            // left, bottom, width, height
            Aspose.Pdf.Drawing.Ellipse outlineEllipse = new Aspose.Pdf.Drawing.Ellipse(250, 150, 200, 150);
            outlineEllipse.GraphInfo.Color = Aspose.Pdf.Color.Red;       // Border color
            outlineEllipse.GraphInfo.LineWidth = 2;                       // Border thickness
            // No fill color – left as transparent
            graph.Shapes.Add(outlineEllipse);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Optional title
            TextFragment title = new TextFragment("Filled and Unfilled Shapes Example");
            title.Position = new Position(0, 420);
            title.TextState.FontSize = 16;
            title.TextState.FontStyle = FontStyles.Bold;
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            page.Paragraphs.Add(title);

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}
