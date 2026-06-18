using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPortfolio = "portfolio.pdf";
        const string outputPortfolio = "portfolio_with_template.pdf";

        if (!File.Exists(inputPortfolio))
        {
            Console.Error.WriteLine($"File not found: {inputPortfolio}");
            return;
        }

        using (Document doc = new Document(inputPortfolio))
        {
            foreach (Page page in doc.Pages)
            {
                // Graph container sized to the page
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // ---- Background rectangle (light gray) ----
                // Use Aspose.Pdf.Drawing.Rectangle (shape) – not Aspose.Pdf.Rectangle (annotation)
                var background = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)page.PageInfo.Width,
                    (float)page.PageInfo.Height);
                background.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.DarkGray,
                    LineWidth = 0f // float literal as required by GraphInfo
                };
                graph.Shapes.Add(background);

                // ---- Header line (blue, 2pt) ----
                // Line expects a float[]; cast page dimensions to float
                float[] linePoints = {
                    0f,
                    (float)(page.PageInfo.Height - 50),
                    (float)page.PageInfo.Width,
                    (float)(page.PageInfo.Height - 50)
                };
                var headerLine = new Line(linePoints);
                headerLine.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,
                    LineWidth = 2f // float literal
                };
                graph.Shapes.Add(headerLine);

                // Add the Graph (containing the shapes) to the page's content
                page.Paragraphs.Add(graph);
            }

            // Save the modified portfolio
            doc.Save(outputPortfolio);
        }

        Console.WriteLine($"Portfolio saved with visual template: {outputPortfolio}");
    }
}