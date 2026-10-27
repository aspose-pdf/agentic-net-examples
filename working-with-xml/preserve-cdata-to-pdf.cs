using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

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

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the PDF
            Page page = pdfDoc.Pages.Add();

            // Process the XML nodes recursively and add them to the PDF page
            ProcessXmlNode(xmlDoc.DocumentElement, page, 0);

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created at '{pdfPath}'.");
    }

    /// <summary>
    /// Recursively processes an XmlNode and adds formatted text fragments to the PDF page.
    /// </summary>
    /// <param name="node">Current XML node.</param>
    /// <param name="page">PDF page to which text fragments are added.</param>
    /// <param name="indentLevel">Current indentation level (for visual hierarchy).</param>
    private static void ProcessXmlNode(XmlNode node, Page page, int indentLevel)
    {
        if (node == null) return;

        // Indentation string
        string indent = new string(' ', indentLevel * 4);

        // Handle element nodes
        if (node.NodeType == XmlNodeType.Element)
        {
            // Write the start tag
            TextFragment startTag = new TextFragment($"{indent}<{node.Name}>");
            startTag.TextState.Font = FontRepository.FindFont("Helvetica");
            startTag.TextState.FontSize = 12;
            startTag.TextState.FontStyle = FontStyles.Bold;
            page.Paragraphs.Add(startTag);

            // Process child nodes
            foreach (XmlNode child in node.ChildNodes)
            {
                ProcessXmlNode(child, page, indentLevel + 1);
            }

            // Write the end tag
            TextFragment endTag = new TextFragment($"{indent}</{node.Name}>");
            endTag.TextState.Font = FontRepository.FindFont("Helvetica");
            endTag.TextState.FontSize = 12;
            endTag.TextState.FontStyle = FontStyles.Bold;
            page.Paragraphs.Add(endTag);
        }
        // Handle CDATA sections
        else if (node.NodeType == XmlNodeType.CDATA)
        {
            // Render CDATA content with a distinct background color
            TextFragment cdataFragment = new TextFragment($"{indent}<![CDATA[{node.Value}]]>");
            cdataFragment.TextState.Font = FontRepository.FindFont("Courier New");
            cdataFragment.TextState.FontSize = 12;
            cdataFragment.TextState.ForegroundColor = Color.FromRgb(0.2, 0.2, 0.8);
            cdataFragment.TextState.BackgroundColor = Color.LightGray;
            page.Paragraphs.Add(cdataFragment);
        }
        // Handle text nodes (including whitespace)
        else if (node.NodeType == XmlNodeType.Text)
        {
            // Trim to avoid adding empty fragments
            string text = node.Value?.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                TextFragment textFragment = new TextFragment($"{indent}{text}");
                textFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                textFragment.TextState.FontSize = 12;
                page.Paragraphs.Add(textFragment);
            }
        }
        // Handle comments (optional)
        else if (node.NodeType == XmlNodeType.Comment)
        {
            TextFragment commentFragment = new TextFragment($"{indent}<!--{node.Value}-->");
            commentFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            commentFragment.TextState.FontSize = 12;
            commentFragment.TextState.FontStyle = FontStyles.Italic;
            commentFragment.TextState.ForegroundColor = Color.Gray;
            page.Paragraphs.Add(commentFragment);
        }
    }
}