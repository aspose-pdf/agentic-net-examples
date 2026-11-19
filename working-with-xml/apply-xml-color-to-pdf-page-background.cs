using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "colored_output.pdf";
        const string colorXml  = "colors.xml";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(colorXml))
        {
            Console.Error.WriteLine("Input PDF or XML color file not found.");
            return;
        }

        // Load a hex color string (e.g. "#FFAA00") from the XML
        string hexColor = LoadHexColor(colorXml);
        if (string.IsNullOrEmpty(hexColor))
        {
            Console.Error.WriteLine("No <PageColor> element found in XML.");
            return;
        }

        // Convert hex to normalized RGB values (0‑1) required by Aspose.Pdf.Color
        (double r, double g, double b) = HexToRgb(hexColor);

        // Open the PDF and apply the background color to each page
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // The background color is set on the Page object itself, not on PageInfo
                page.Background = Aspose.Pdf.Color.FromRgb(r, g, b);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background color {hexColor} applied to all pages. Saved as '{outputPdf}'.");
    }

    // Reads the <PageColor> element value from the XML file
    static string LoadHexColor(string xmlPath)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        // Expected structure: <Colors><PageColor>#RRGGBB</PageColor></Colors>
        XElement elem = xdoc.Root.Element("PageColor");
        return elem?.Value.Trim();
    }

    // Converts a hex string to normalized RGB components (0‑1 range)
    static (double r, double g, double b) HexToRgb(string hex)
    {
        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        if (hex.Length != 6)
            return (0, 0, 0); // fallback to black on invalid format

        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);

        return (r / 255.0, g / 255.0, b / 255.0);
    }
}
