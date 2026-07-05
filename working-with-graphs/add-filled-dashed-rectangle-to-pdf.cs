using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "filled_rectangle.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            // Graph constructor expects double values
            Graph graph = new Graph(400.0, 200.0);

            // Use Aspose.Pdf.Drawing.Rectangle for shapes inside a Graph
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.DarkBlue,
                LineWidth = 2f,                     // float literal as required
                DashArray = new int[] { 5, 3 }      // dash pattern (dash, gap) – int[] required
            };

            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled, dashed rectangle saved to '{outputPath}'.");
    }
}
