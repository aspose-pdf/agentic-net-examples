using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

// Alias to avoid ambiguity between System.IO.Path and Aspose.Pdf.Drawing.Path
using SystemPath = System.IO.Path;

class Program
{
    static void Main()
    {
        // Paths – adjust to your environment
        string dataDir   = @"YOUR_DATA_DIRECTORY";
        string mhtFile   = SystemPath.Combine(dataDir, "input.mht");
        string outputPdf = SystemPath.Combine(dataDir, "output.pdf");

        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }

        // Load the MHT file into a PDF document
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        using (Document doc = new Document(mhtFile, loadOptions))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // Add vector graphics (rectangle, ellipse, line) using Graph
            // -------------------------------------------------
            // Use double parameters as the Graph constructor now expects double values
            Graph graph = new Graph(500.0, 400.0); // container size

            // Rectangle shape
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 300, 200, 150);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rectShape);

            // Ellipse shape
            Ellipse ellipse = new Ellipse(300, 200, 150, 100);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // Line shape
            // Use float literals (suffix 'f') to match the expected float[] type
            float[] linePoints = new float[] { 100f, 100f, 400f, 350f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // -------------------------------------------------
            // Add a tagged Figure element for accessibility
            // -------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Shapes");

            StructureElement root = tagged.RootElement;

            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Illustration containing rectangle, ellipse, and line.";
            root.AppendChild(figure); // attach to the structure tree

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
