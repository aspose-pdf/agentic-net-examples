using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string configPath = "shapeconfig.txt";   // file containing a color name, e.g., LightGray
        const string outputPath = "shapes.pdf";

        // Read the fill color name from the configuration file
        string colorName = File.Exists(configPath) ? File.ReadAllText(configPath).Trim() : "LightGray";

        // Resolve the color name to an Aspose.Pdf.Color instance via reflection
        Aspose.Pdf.Color fillColor = ResolveColor(colorName) ?? Aspose.Pdf.Color.LightGray;

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Use the double‑based Graph constructor (deprecated float constructor removed)
            Graph graph = new Graph(400.0, 200.0);

            // Create a rectangle shape (left, bottom, width, height) – use the drawing rectangle type
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 150f);

            // Set visual properties through GraphInfo (LineWidth is a float)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = fillColor,          // use the color from config
                Color = Aspose.Pdf.Color.Black, // stroke color
                LineWidth = 2f
            };

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with shape saved to '{outputPath}'.");
    }

    // Helper method to map a color name to Aspose.Pdf.Color static property
    static Aspose.Pdf.Color ResolveColor(string name)
    {
        if (string.IsNullOrEmpty(name))
            return null;

        // Look for a public static property with the given name (case‑insensitive)
        PropertyInfo prop = typeof(Aspose.Pdf.Color).GetProperty(name,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

        return prop?.GetValue(null) as Aspose.Pdf.Color;
    }
}
