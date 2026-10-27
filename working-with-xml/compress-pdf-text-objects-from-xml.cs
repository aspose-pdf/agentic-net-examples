using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Ensure the document has a root element
        if (xmlDoc.DocumentElement == null)
        {
            Console.Error.WriteLine("The XML document does not contain a root element.");
            return;
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a single page
            Page page = pdfDoc.Pages.Add();

            // Retrieve a font for text rendering
            Aspose.Pdf.Text.Font font = FontRepository.FindFont("Arial");

            // Recursively add XML nodes as text fragments
            AddXmlNodeToPage(xmlDoc.DocumentElement, page, font, 0);

            // Optimize resources to compress streams (including text objects)
            // The parameterless overload compresses streams without requiring the
            // OptimizationOptions class, which may be unavailable in some library versions.
            pdfDoc.OptimizeResources();

            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created at '{pdfPath}'.");
    }

    // Helper method to add an XML node and its children to the PDF page
    static void AddXmlNodeToPage(XmlNode node, Page page, Aspose.Pdf.Text.Font font, int indentLevel)
    {
        // Prepare indented text representing the XML element
        string indent = new string(' ', indentLevel * 4);
        string content = $"{indent}<{node.Name}> {node.InnerText}";

        // Create a text fragment with the content
        TextFragment fragment = new TextFragment(content);
        fragment.TextState.Font = font;
        fragment.TextState.FontSize = 12;
        fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

        // Add the fragment to the page
        page.Paragraphs.Add(fragment);

        // Process child elements recursively
        foreach (XmlNode child in node.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.Element)
            {
                AddXmlNodeToPage(child, page, font, indentLevel + 1);
            }
        }
    }
}
