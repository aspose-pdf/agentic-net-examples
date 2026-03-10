using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string graphicsXmlPath = "graphics.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(graphicsXmlPath))
        {
            Console.Error.WriteLine($"Graphics definition XML not found: {graphicsXmlPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Work with the first page (adjust index as needed)
            Page page = doc.Pages[1];

            // Parse the XML that defines graphic elements
            XDocument xmlDoc = XDocument.Load(graphicsXmlPath);

            // Create a Graph container – use double literals for width/height as required by the newer constructor
            Graph graph = new Graph(500.0, 500.0);

            foreach (XElement elem in xmlDoc.Root.Elements())
            {
                switch (elem.Name.LocalName.ToLower())
                {
                    case "rectangle":
                        // Expected attributes: left, bottom, width, height, fillcolor, strokecolor, linewidth
                        float rectLeft   = float.Parse(elem.Attribute("left")?.Value ?? "0");
                        float rectBottom = float.Parse(elem.Attribute("bottom")?.Value ?? "0");
                        float rectWidth  = float.Parse(elem.Attribute("width")?.Value ?? "100");
                        float rectHeight = float.Parse(elem.Attribute("height")?.Value ?? "50");

                        // Use the drawing Rectangle (Aspose.Pdf.Drawing.Rectangle)
                        Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(rectLeft, rectBottom, rectWidth, rectHeight);
                        rect.GraphInfo = new GraphInfo
                        {
                            FillColor = ParseColor(elem.Attribute("fillcolor")?.Value),
                            Color     = ParseColor(elem.Attribute("strokecolor")?.Value),
                            // LineWidth expects a float – use float.Parse or cast explicitly
                            LineWidth = float.Parse(elem.Attribute("linewidth")?.Value ?? "1")
                        };
                        graph.Shapes.Add(rect);
                        break;

                    case "ellipse":
                        // Expected attributes: left, bottom, width, height, fillcolor, strokecolor, linewidth
                        float ellLeft   = float.Parse(elem.Attribute("left")?.Value ?? "0");
                        float ellBottom = float.Parse(elem.Attribute("bottom")?.Value ?? "0");
                        float ellWidth  = float.Parse(elem.Attribute("width")?.Value ?? "100");
                        float ellHeight = float.Parse(elem.Attribute("height")?.Value ?? "50");

                        Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(ellLeft, ellBottom, ellWidth, ellHeight);
                        ellipse.GraphInfo = new GraphInfo
                        {
                            FillColor = ParseColor(elem.Attribute("fillcolor")?.Value),
                            Color     = ParseColor(elem.Attribute("strokecolor")?.Value),
                            LineWidth = float.Parse(elem.Attribute("linewidth")?.Value ?? "1")
                        };
                        graph.Shapes.Add(ellipse);
                        break;

                    case "line":
                        // Expected attributes: x1, y1, x2, y2, color, linewidth
                        float x1 = float.Parse(elem.Attribute("x1")?.Value ?? "0");
                        float y1 = float.Parse(elem.Attribute("y1")?.Value ?? "0");
                        float x2 = float.Parse(elem.Attribute("x2")?.Value ?? "100");
                        float y2 = float.Parse(elem.Attribute("y2")?.Value ?? "0");

                        float[] linePoints = { x1, y1, x2, y2 };
                        Line line = new Line(linePoints);
                        line.GraphInfo = new GraphInfo
                        {
                            Color     = ParseColor(elem.Attribute("color")?.Value),
                            LineWidth = float.Parse(elem.Attribute("linewidth")?.Value ?? "1")
                        };
                        graph.Shapes.Add(line);
                        break;

                    // Additional shape types can be added here following the same pattern
                }
            }

            // Add the constructed graph to the page
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Graphics inserted and saved to '{outputPdfPath}'.");
    }

    // Helper to convert a color string (e.g., "Red", "#FF0000") to Aspose.Pdf.Color
    private static Aspose.Pdf.Color ParseColor(string colorStr)
    {
        if (string.IsNullOrWhiteSpace(colorStr))
            return Aspose.Pdf.Color.Black; // default

        // Try named colors first
        try
        {
            return (Aspose.Pdf.Color)Enum.Parse(typeof(Aspose.Pdf.Color), colorStr, true);
        }
        catch
        {
            // Fallback to hex parsing (expects format "#RRGGBB")
            if (colorStr.StartsWith("#") && colorStr.Length == 7)
            {
                int r = Convert.ToInt32(colorStr.Substring(1, 2), 16);
                int g = Convert.ToInt32(colorStr.Substring(3, 2), 16);
                int b = Convert.ToInt32(colorStr.Substring(5, 2), 16);
                return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
            }
        }

        // If parsing fails, return black
        return Aspose.Pdf.Color.Black;
    }
}
