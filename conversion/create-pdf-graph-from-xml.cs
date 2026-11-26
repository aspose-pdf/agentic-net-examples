using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string xmlPath = "graph.xml";
        const string pdfPath = "graph.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        XDocument xDoc = XDocument.Load(xmlPath);
        if (xDoc.Root == null)
        {
            Console.Error.WriteLine("Invalid XML: missing root element.");
            return;
        }

        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();

            // Graph constructor accepts only width and height (float or double).
            // Position is set via Left/Top properties.
            float pageWidth = (float)page.PageInfo.Width;
            float pageHeight = (float)page.PageInfo.Height;
            Graph graph = new Graph(pageWidth, pageHeight)
            {
                Left = 0,
                Top = 0
            };

            // Iterate over each Circle element in the XML
            foreach (XElement circleElem in xDoc.Root.Elements("Circle"))
            {
                // Parse attributes; cast to float because shape constructors require float.
                float x = (float?)circleElem.Attribute("X") ?? 0f;
                float y = (float?)circleElem.Attribute("Y") ?? 0f;
                float radius = (float?)circleElem.Attribute("Radius") ?? 10f;
                string colorHex = (string)circleElem.Attribute("Color") ?? "#000000";

                // Create the Circle shape (float parameters)
                Circle circle = new Circle(x, y, radius);

                // Set fill color using Aspose.Pdf.Color.FromRgb (expects double 0‑1 range).
                if (colorHex.StartsWith("#") && colorHex.Length == 7)
                {
                    int r = Convert.ToInt32(colorHex.Substring(1, 2), 16);
                    int g = Convert.ToInt32(colorHex.Substring(3, 2), 16);
                    int b = Convert.ToInt32(colorHex.Substring(5, 2), 16);
                    circle.GraphInfo.Color = Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
                }
                else
                {
                    circle.GraphInfo.Color = Aspose.Pdf.Color.Black;
                }

                // Add the circle to the graph
                graph.Shapes.Add(circle);
            }

            // Add the graph to the page and save the PDF
            page.Paragraphs.Add(graph);
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with graph saved to '{pdfPath}'.");
    }
}
