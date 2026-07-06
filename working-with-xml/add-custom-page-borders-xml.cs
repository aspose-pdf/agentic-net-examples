using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class AddCustomPageBorders
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_borders.pdf";
        const string borderXml = "border_definition.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(borderXml))
        {
            Console.Error.WriteLine($"Border definition XML not found: {borderXml}");
            return;
        }

        // Load border definition from XML
        // Expected XML format:
        // <Border>
        //   <Left>50</Left>
        //   <Bottom>50</Bottom>
        //   <Width>500</Width>
        //   <Height>700</Height>
        //   <Color>#FF0000</Color>   <!-- Hex RGB -->
        //   <LineWidth>2</LineWidth>
        // </Border>
        double left = 0, bottom = 0, width = 0, height = 0;
        Aspose.Pdf.Color borderColor = Aspose.Pdf.Color.Black;
        double lineWidth = 1;

        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(borderXml);
            XmlNode root = xmlDoc.SelectSingleNode("/Border");
            left = double.Parse(root.SelectSingleNode("Left")?.InnerText ?? "0");
            bottom = double.Parse(root.SelectSingleNode("Bottom")?.InnerText ?? "0");
            width = double.Parse(root.SelectSingleNode("Width")?.InnerText ?? "0");
            height = double.Parse(root.SelectSingleNode("Height")?.InnerText ?? "0");
            string colorHex = root.SelectSingleNode("Color")?.InnerText ?? "#000000";
            // Convert hex to Aspose.Pdf.Color
            if (colorHex.StartsWith("#")) colorHex = colorHex.Substring(1);
            int r = Convert.ToInt32(colorHex.Substring(0, 2), 16);
            int g = Convert.ToInt32(colorHex.Substring(2, 2), 16);
            int b = Convert.ToInt32(colorHex.Substring(4, 2), 16);
            borderColor = Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
            lineWidth = double.Parse(root.SelectSingleNode("LineWidth")?.InnerText ?? "1");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse border XML: {ex.Message}");
            return;
        }

        // Open the PDF, add borders to each page
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a Graph container sized to the page (Graph expects double values)
                Graph graph = new Graph(page.Rect.Width, page.Rect.Height);

                // Define a rectangle shape that represents the border
                // Rectangle constructor expects float values, so cast accordingly
                var rectShape = new Aspose.Pdf.Drawing.Rectangle(
                    (float)left,
                    (float)bottom,
                    (float)width,
                    (float)height);

                rectShape.GraphInfo = new GraphInfo
                {
                    Color = borderColor,                // Stroke color
                    LineWidth = (float)lineWidth,       // Stroke thickness (float required)
                    FillColor = Aspose.Pdf.Color.Transparent // No fill
                };

                // Add the rectangle to the graph
                graph.Shapes.Add(rectShape);

                // Position the graph at (0,0) relative to the page
                page.Paragraphs.Add(graph);
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom borders: {outputPdf}");
    }
}
