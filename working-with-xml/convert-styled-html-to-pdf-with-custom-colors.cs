using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string styleXml = "style.xml";
        const string outputPdf = "styled_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(styleXml))
        {
            Console.Error.WriteLine($"Style XML not found: {styleXml}");
            return;
        }

        // Load the custom color defined in the XML (e.g., <Style><TextColor>#FF0000</TextColor></Style>)
        Color customColor = LoadColorFromStyle(styleXml);

        using (Document doc = new Document(inputPdf))
        {
            if (customColor != null)
            {
                ApplyColorToAllText(doc, customColor);
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"Styled PDF saved to '{outputPdf}'.");
    }

    // Reads the first <TextColor> element from the XML and converts it to an Aspose.Pdf.Color.
    private static Color LoadColorFromStyle(string xmlPath)
    {
        try
        {
            XDocument xdoc = XDocument.Load(xmlPath);
            var colorElement = xdoc.Root?.Element("TextColor");
            if (colorElement == null)
                return null;

            string hex = colorElement.Value.TrimStart('#');
            if (hex.Length == 6) // RGB
            {
                int r = Convert.ToInt32(hex.Substring(0, 2), 16);
                int g = Convert.ToInt32(hex.Substring(2, 2), 16);
                int b = Convert.ToInt32(hex.Substring(4, 2), 16);
                return Color.FromRgb(r, g, b);
            }
            else if (hex.Length == 8) // ARGB
            {
                int a = Convert.ToInt32(hex.Substring(0, 2), 16);
                int r = Convert.ToInt32(hex.Substring(2, 2), 16);
                int g = Convert.ToInt32(hex.Substring(4, 2), 16);
                int b = Convert.ToInt32(hex.Substring(6, 2), 16);
                return Color.FromArgb(a, r, g, b);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read style XML: {ex.Message}");
        }
        return null;
    }

    // Applies the supplied color to every TextFragment in the document.
    private static void ApplyColorToAllText(Document doc, Color color)
    {
        foreach (Page page in doc.Pages)
        {
            // Absorb all text fragments on the page.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            page.Accept(absorber);
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.ForegroundColor = color;
            }
        }
    }
}
