using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "Report.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Graph layout parameters
            double graphX = 50;          // left margin of the graph
            double graphY = 500;         // baseline of the bars
            double barWidth = 40;        // width of each bar
            double barSpacing = 20;      // space between bars

            // Sample data categories
            var categories = new[]
            {
                new { Label = "Category A", Value = 120, Color = Aspose.Pdf.Color.FromRgb(0.2, 0.6, 0.8) },
                new { Label = "Category B", Value = 80,  Color = Aspose.Pdf.Color.FromRgb(0.8, 0.3, 0.3) },
                new { Label = "Category C", Value = 150, Color = Aspose.Pdf.Color.FromRgb(0.4, 0.8, 0.4) },
                new { Label = "Category D", Value = 60,  Color = Aspose.Pdf.Color.FromRgb(0.9, 0.7, 0.2) }
            };

            // Determine the maximum value for scaling the bars
            double maxValue = 0;
            foreach (var c in categories)
                if (c.Value > maxValue) maxValue = c.Value;

            // Create a Graph that will hold all rectangle shapes (bars + legend boxes)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Draw each bar as a colored rectangle
            for (int i = 0; i < categories.Length; i++)
            {
                double barHeight = (categories[i].Value / maxValue) * 300; // scale to 300 units height
                double llx = graphX + i * (barWidth + barSpacing);
                double lly = graphY - barHeight;
                double urx = llx + barWidth;
                double ury = graphY;

                // Rectangle shape representing the bar
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle((float)llx, (float)lly, (float)urx, (float)ury);
                rect.GraphInfo.FillColor = categories[i].Color;          // fill with category colour
                rect.GraphInfo.Color = Aspose.Pdf.Color.Black;          // optional border colour
                graph.Shapes.Add(rect);

                // Label under the bar
                TextFragment label = new TextFragment(categories[i].Label)
                {
                    Position = new Position((float)llx, (float)(graphY - 20)),
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                page.Paragraphs.Add(label);
            }

            // Draw legend on the right side of the graph
            double legendX = graphX + categories.Length * (barWidth + barSpacing) + 30;
            double legendY = graphY;
            double legendItemHeight = 20;
            double legendItemSpacing = 10;

            for (int i = 0; i < categories.Length; i++)
            {
                double boxSize = 12;
                double boxX = legendX;
                double boxY = legendY - i * (legendItemHeight + legendItemSpacing) - boxSize;

                // Colored box for the legend entry
                Aspose.Pdf.Drawing.Rectangle colorBox = new Aspose.Pdf.Drawing.Rectangle((float)boxX, (float)boxY, (float)(boxX + boxSize), (float)(boxY + boxSize));
                colorBox.GraphInfo.FillColor = categories[i].Color;
                colorBox.GraphInfo.Color = Aspose.Pdf.Color.Black;
                graph.Shapes.Add(colorBox);

                // Text label next to the colored box
                TextFragment legendText = new TextFragment(categories[i].Label)
                {
                    Position = new Position((float)(boxX + boxSize + 5), (float)(boxY + 2)),
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                page.Paragraphs.Add(legendText);
            }

            // Add the graph (containing all rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF report
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF report generated: {System.IO.Path.GetFullPath(outputPath)}");
    }
}
