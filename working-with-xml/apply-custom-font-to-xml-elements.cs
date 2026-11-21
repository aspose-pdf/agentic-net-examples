using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class XmlToPdfGenerator
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";
        const string fontPath = "CustomFont.ttf"; // path to the custom TrueType/OpenType font

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the XML document
        XDocument xDoc = XDocument.Load(xmlPath);

        // Create a new PDF document
        Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

        // Add a single page (more pages can be added as needed)
        Aspose.Pdf.Page page = pdfDoc.Pages.Add();

        // Load the custom font
        Aspose.Pdf.Text.Font customFont = Aspose.Pdf.Text.FontRepository.OpenFont(fontPath);

        // Process XML elements in document order
        foreach (XElement element in xDoc.Descendants())
        {
            // Skip elements that contain child elements – we only render leaf text nodes
            if (element.HasElements) continue;

            // Create a text fragment with the element's text content
            Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment(element.Value);

            // Apply the custom font to specific elements (e.g., <title>)
            if (string.Equals(element.Name.LocalName, "title", StringComparison.OrdinalIgnoreCase))
            {
                fragment.TextState.Font = customFont;
                fragment.TextState.FontSize = 16; // larger size for titles
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0, 0, 0.8); // optional color
            }
            else
            {
                // Default styling for other text
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            }

            // Add a line break after each fragment for readability
            fragment.Text += "\n";

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);
        }

        // Save the generated PDF
        pdfDoc.Save(pdfPath);
        Console.WriteLine($"PDF generated successfully at '{pdfPath}'.");
    }
}