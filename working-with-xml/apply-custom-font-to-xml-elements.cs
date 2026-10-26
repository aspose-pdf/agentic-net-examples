using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath        = "input.xml";
        const string outputPdfPath  = "output.pdf";
        const string customFontPath = "custom.ttf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font file not found: {customFontPath}");
            return;
        }

        // Load the XML document.
        XDocument xDoc = XDocument.Load(xmlPath);
        if (xDoc.Root == null)
        {
            Console.Error.WriteLine("XML document has no root element.");
            return;
        }

        // Register the custom font using the modern FontRepository API.
        // "CustomFont" is an arbitrary name used to reference the font later.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("CustomFont", customFontPath));
        Font customFont = FontRepository.FindFont("CustomFont");

        // Create a new PDF document.
        using (Document pdfDoc = new Document())
        {
            // Add a single page to hold the content.
            Page page = pdfDoc.Pages.Add();

            // Recursively process XML elements and add them to the PDF.
            ProcessElement(xDoc.Root, page, customFont);

            // Save the PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated at '{outputPdfPath}'.");
    }

    // Recursively traverses XML elements, creating a TextFragment for each.
    // Elements named "Important" (example) will use the custom font.
    static void ProcessElement(XElement element, Page page, Font customFont)
    {
        if (element == null) return;

        // Create a text fragment for the element's name.
        TextFragment nameFragment = new TextFragment($"<{element.Name}>");
        nameFragment.TextState.FontSize = 12;
        // Use custom font for elements named "Important".
        if (string.Equals(element.Name.LocalName, "Important", StringComparison.OrdinalIgnoreCase))
            nameFragment.TextState.Font = customFont;
        page.Paragraphs.Add(nameFragment);

        // If the element has a value (text content), add it.
        if (!string.IsNullOrWhiteSpace(element.Value) && !element.HasElements)
        {
            TextFragment valueFragment = new TextFragment(element.Value.Trim());
            valueFragment.TextState.FontSize = 12;
            if (string.Equals(element.Name.LocalName, "Important", StringComparison.OrdinalIgnoreCase))
                valueFragment.TextState.Font = customFont;
            page.Paragraphs.Add(valueFragment);
        }

        // Process child elements.
        foreach (XElement child in element.Elements())
        {
            ProcessElement(child, page, customFont);
        }

        // Add a closing tag fragment.
        TextFragment closeFragment = new TextFragment($"</{element.Name}>");
        closeFragment.TextState.FontSize = 12;
        if (string.Equals(element.Name.LocalName, "Important", StringComparison.OrdinalIgnoreCase))
            closeFragment.TextState.Font = customFont;
        page.Paragraphs.Add(closeFragment);
    }
}
