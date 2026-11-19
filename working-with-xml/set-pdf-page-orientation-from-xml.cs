using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "layout.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML layout file not found: {xmlPath}");
            return;
        }

        // Load the XML that defines page layout attributes
        XDocument layoutDoc = XDocument.Load(xmlPath);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Iterate over each <Page> element in the XML (1‑based indexing is not required here)
            foreach (XElement pageNode in layoutDoc.Root.Elements("Page"))
            {
                // Add a new page to the document
                Page page = pdfDoc.Pages.Add();

                // Parse width and height attributes; fall back to A4 dimensions if missing
                double width = double.TryParse(pageNode.Attribute("width")?.Value, out double w) ? w : 842;   // default landscape A4 width
                double height = double.TryParse(pageNode.Attribute("height")?.Value, out double h) ? h : 595; // default landscape A4 height

                // Determine orientation from the XML attribute and adjust dimensions if necessary
                string orientationAttr = pageNode.Attribute("orientation")?.Value?.Trim().ToLowerInvariant();
                if (orientationAttr == "landscape")
                {
                    // Ensure width is greater than height for landscape
                    if (width < height)
                    {
                        double tmp = width;
                        width = height;
                        height = tmp;
                    }
                }
                else if (orientationAttr == "portrait")
                {
                    // Ensure height is greater than width for portrait
                    if (height < width)
                    {
                        double tmp = width;
                        width = height;
                        height = tmp;
                    }
                }
                // If the attribute is missing or unrecognised we keep the values as‑is (the caller can supply any size).

                // Apply the calculated dimensions – PageInfo does not expose an Orientation property.
                page.PageInfo.Width = width;
                page.PageInfo.Height = height;

                // Optional: add a label showing the applied orientation based on dimensions
                string appliedOrientation = width > height ? "Landscape" : "Portrait";
                TextFragment label = new TextFragment($"Page {pdfDoc.Pages.Count} - {appliedOrientation}");
                // Position uses Aspose.Pdf.Text.Position (fully qualified via using)
                label.Position = new Position(50, height - 50);
                page.Paragraphs.Add(label);
            }

            // Save the resulting PDF; no SaveOptions needed because the output is PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with custom orientations: {outputPdf}");
    }
}
