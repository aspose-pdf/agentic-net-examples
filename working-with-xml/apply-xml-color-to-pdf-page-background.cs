using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string colorXmlPath   = "backgroundColor.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(colorXmlPath))
        {
            Console.Error.WriteLine($"Color XML not found: {colorXmlPath}");
            return;
        }

        // Load the XML and extract the color value (e.g., <BackgroundColor>#FFAA00</BackgroundColor>)
        string hexColor = LoadHexColorFromXml(colorXmlPath);
        if (string.IsNullOrEmpty(hexColor))
        {
            Console.Error.WriteLine("Failed to read background color from XML.");
            return;
        }

        // Convert hex string to Aspose.Pdf.Color (values are 0‑1 doubles)
        Aspose.Pdf.Color bgColor = ColorFromHex(hexColor);

        // Open the PDF, apply the background color to each page, and save.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                // Set the background color via the Page object (not PageInfo)
                page.Background = bgColor;
            }

            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background color applied and saved to '{outputPdfPath}'.");
    }

    // Reads the first <BackgroundColor> element value from the XML file.
    private static string LoadHexColorFromXml(string xmlPath)
    {
        try
        {
            XDocument doc = XDocument.Load(xmlPath);
            XElement colorElement = doc.Root.Element("BackgroundColor");
            return colorElement?.Value.Trim();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading XML: {ex.Message}");
            return null;
        }
    }

    // Converts a hex color string (e.g., "#RRGGBB" or "RRGGBB") to Aspose.Pdf.Color.
    private static Aspose.Pdf.Color ColorFromHex(string hex)
    {
        // Remove leading '#', if present
        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        if (hex.Length != 6)
            throw new ArgumentException("Hex color must be 6 characters long.");

        // Parse RGB components
        byte r = Convert.ToByte(hex.Substring(0, 2), 16);
        byte g = Convert.ToByte(hex.Substring(2, 2), 16);
        byte b = Convert.ToByte(hex.Substring(4, 2), 16);

        // Aspose.Pdf.Color expects values in the range 0‑1
        return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
    }
}
