using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_custom_font.pdf";
        const string fontPath   = "MyCustomFont.ttf";
        const string fontAlias  = "MyCustomFont"; // alias used for substitution

        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Register the custom TrueType font so that Aspose.Pdf can resolve it by alias.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontAlias, fontPath));

        // Retrieve the Font object that will be embedded when used.
        Font customFont = FontRepository.FindFont(fontAlias);
        if (customFont == null)
        {
            Console.Error.WriteLine($"Failed to load font alias: {fontAlias}");
            return;
        }

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a page to the document.
            Page page = doc.Pages.Add();

            // Create a graph (width x height) – use double values as the float overload is obsolete.
            Graph graph = new Graph(400.0, 300.0);

            // Optional: give the graph a thin gray border so the area is visible.
            graph.Border = new BorderInfo(BorderSide.All, Color.Gray);

            // Set the graph title using the custom font.
            TextFragment title = new TextFragment("Sample Chart");
            title.TextState.Font = customFont;
            title.TextState.FontSize = 18;
            graph.Title = title;

            // -----------------------------------------------------------------
            // Add a simple line series to the graph.
            // The points are expressed in the graph's coordinate system (0‑400, 0‑300).
            // -----------------------------------------------------------------
            float[] linePoints = new float[] { 50, 250, 100, 200, 150, 220, 200, 180, 250, 210 };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Add a legend entry for the series – also using the custom font.
            // A TextFragment can be placed on the page; here we add it directly to the page
            // because Graph only supports a single Title TextFragment.
            TextFragment legend = new TextFragment("Series 1");
            legend.TextState.Font = customFont;
            legend.TextState.FontSize = 10;
            // Position the legend near the top‑right corner of the graph.
            legend.Position = new Position(300, 260);
            page.Paragraphs.Add(legend);

            // Add the graph to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF; the custom font will be embedded automatically.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
