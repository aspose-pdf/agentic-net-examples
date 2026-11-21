using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xmlConfigPath = "border.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlConfigPath))
        {
            Console.Error.WriteLine($"XML config not found: {xmlConfigPath}");
            return;
        }

        // Load border settings from XML
        XDocument config = XDocument.Load(xmlConfigPath);
        XElement borderElem = config.Root.Element("Border");
        string colorHex = borderElem.Element("Color")?.Value ?? "#000000";
        int width = int.Parse(borderElem.Element("Width")?.Value ?? "1");

        // Convert hex color to Aspose.Pdf.Color (RGB values are 0‑1 range)
        Aspose.Pdf.Color borderColor = ColorFromHex(colorHex);

        // Open PDF and apply border to each page
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            foreach (Page page in pdfDoc.Pages)
            {
                // Create a rectangle that covers the whole page
                var pageRect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

                // Use a StampAnnotation (core API) to draw the border
                var borderStamp = new StampAnnotation(page, pageRect)
                {
                    // Make the stamp itself invisible – we only need the border
                    Opacity = 0f,
                    // Set the border colour
                    Color = borderColor
                };

                // Define the border (width and style). Border colour is taken from the annotation's Color property.
                borderStamp.Border = new Border(borderStamp)
                {
                    Width = width,
                    Style = BorderStyle.Solid
                };

                // Add the stamp annotation to the page
                page.Annotations.Add(borderStamp);
            }

            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with custom borders saved to '{outputPdfPath}'.");
    }

    // Helper: convert "#RRGGBB" to Aspose.Pdf.Color
    static Aspose.Pdf.Color ColorFromHex(string hex)
    {
        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        if (hex.Length != 6)
            return Aspose.Pdf.Color.Black;

        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);

        return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
    }
}
