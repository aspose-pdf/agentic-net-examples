using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_text.pdf";

        // Ensure deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400 points, height: 200 points)
            Graph graph = new Graph(400, 200)
            {
                // Position the graph on the page (left: 100, top: 500)
                Left = 100,
                Top = 500
            };

            // Create a rectangle shape inside the graph
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 300, 150);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rectShape);

            // Add the graph (with the rectangle) to the page
            page.Paragraphs.Add(graph);

            // Prepare a TextBuilder to place text fragments on the page
            TextBuilder textBuilder = new TextBuilder(page);

            // First text fragment – Helvetica, 24pt, blue
            TextFragment tf1 = new TextFragment("Hello");
            tf1.Position = new Position(150, 600);
            tf1.TextState.Font = FontRepository.FindFont("Helvetica");
            tf1.TextState.FontSize = 24;
            tf1.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            textBuilder.AppendText(tf1);

            // Second text fragment – Times New Roman, 18pt, dark red
            TextFragment tf2 = new TextFragment("World");
            tf2.Position = new Position(150, 560);
            tf2.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            tf2.TextState.FontSize = 18;
            tf2.TextState.ForegroundColor = Aspose.Pdf.Color.DarkRed;
            textBuilder.AppendText(tf2);

            // Third text fragment – Courier, 14pt, green
            TextFragment tf3 = new TextFragment("Aspose.Pdf");
            tf3.Position = new Position(150, 520);
            tf3.TextState.Font = FontRepository.FindFont("Courier");
            tf3.TextState.FontSize = 14;
            tf3.TextState.ForegroundColor = Aspose.Pdf.Color.Green;
            textBuilder.AppendText(tf3);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}