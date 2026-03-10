using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the XML configuration file
        const string xmlConfigPath = "pageDefinition.xml";
        // Output PDF file
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(xmlConfigPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {xmlConfigPath}");
            return;
        }

        // Load and parse the XML configuration
        XDocument config = XDocument.Load(xmlConfigPath);
        XElement pageElem = config.Root.Element("Page");

        // Extract page dimensions (in points; 1 point = 1/72 inch)
        double width = double.Parse(pageElem.Element("Width")?.Value ?? "595");   // default A4 width
        double height = double.Parse(pageElem.Element("Height")?.Value ?? "842"); // default A4 height

        // Extract margins (in points)
        XElement margins = pageElem.Element("Margins");
        double marginTop = double.Parse(margins.Element("Top")?.Value ?? "36");
        double marginBottom = double.Parse(margins.Element("Bottom")?.Value ?? "36");
        double marginLeft = double.Parse(margins.Element("Left")?.Value ?? "36");
        double marginRight = double.Parse(margins.Element("Right")?.Value ?? "36");

        // Create PageInfo with the extracted settings
        PageInfo pageInfo = new PageInfo
        {
            Width = width,
            Height = height,
            Margin = new MarginInfo
            {
                Top = marginTop,
                Bottom = marginBottom,
                Left = marginLeft,
                Right = marginRight
            }
        };

        // Create a new PDF document and add a page using the PageInfo
        using (Document doc = new Document())
        {
            // Add a blank page first, then assign the prepared PageInfo
            Page page = doc.Pages.Add();
            page.PageInfo = pageInfo;

            // Process content elements defined in the XML (ignore layout elements like Width/Height/Margins)
            foreach (XElement element in pageElem.Elements())
            {
                switch (element.Name.LocalName)
                {
                    case "Text":
                        // Add a simple text fragment
                        string text = element.Value;
                        TextFragment tf = new TextFragment(text);
                        // Optional: set position if provided
                        XElement pos = element.Element("Position");
                        if (pos != null)
                        {
                            double x = double.Parse(pos.Element("X")?.Value ?? "0");
                            double y = double.Parse(pos.Element("Y")?.Value ?? "0");
                            tf.Position = new Position(x, y);
                        }
                        page.Paragraphs.Add(tf);
                        break;

                    case "Image":
                        // Add an image from a file path
                        string imagePath = element.Element("Path")?.Value;
                        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                        {
                            // Define rectangle where the image will be placed
                            double llx = double.Parse(element.Element("Rect")?.Element("LLX")?.Value ?? "0");
                            double lly = double.Parse(element.Element("Rect")?.Element("LLY")?.Value ?? "0");
                            double urx = double.Parse(element.Element("Rect")?.Element("URX")?.Value ?? "100");
                            double ury = double.Parse(element.Element("Rect")?.Element("URY")?.Value ?? "100");
                            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                            page.AddImage(imagePath, rect);
                        }
                        break;

                    // Additional content types (e.g., tables, shapes) can be handled here
                }
            }

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdfPath}");
    }
}
