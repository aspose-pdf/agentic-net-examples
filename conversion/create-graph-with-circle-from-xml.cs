using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document that will provide data for the graph
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // ---------------------------------------------------------------------
        // Extract information from the XML. The example expects a structure like:
        // <Graph>
        //   <Title>Sample Graph</Title>
        //   <Circle X="200" Y="150" Radius="50" />
        // </Graph>
        // ---------------------------------------------------------------------
        string title = "Graph";
        float circleX = 200f;
        float circleY = 150f;
        float circleRadius = 50f;

        XmlNode titleNode = xmlDoc.SelectSingleNode("//Title");
        if (titleNode != null)
            title = titleNode.InnerText.Trim();

        XmlNode circleNode = xmlDoc.SelectSingleNode("//Circle");
        if (circleNode != null)
        {
            if (float.TryParse(circleNode.Attributes["X"]?.Value, out float x))
                circleX = x;
            if (float.TryParse(circleNode.Attributes["Y"]?.Value, out float y))
                circleY = y;
            if (float.TryParse(circleNode.Attributes["Radius"]?.Value, out float r))
                circleRadius = r;
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();

            // Graph constructor takes only width and height. Position is set via Left/Top.
            var graph = new Graph(400.0, 300.0)   // width, height
            {
                Left = 100,
                Top = 500,
                Title = new TextFragment(title) // GraphInfo.Title is a TextFragment
            };

            // Create a Circle shape and add it to the graph.
            var circle = new Circle(circleX, circleY, circleRadius);
            // Optional styling (stroke and fill)
            circle.GraphInfo.Color = Color.Black;
            // Use the 4‑parameter overload of FromArgb to set a semi‑transparent LightBlue fill.
            circle.GraphInfo.FillColor = Color.FromArgb(64, 173, 216, 230); // 25% opacity LightBlue
            graph.Shapes.Add(circle);

            // Add the graph to the page and save the document.
            page.Paragraphs.Add(graph);
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
