using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the template PDF, original XML, modified XML, and final PDF
        const string pdfTemplatePath = "template.pdf";
        const string xmlInputPath = "content.xml";
        const string xmlModifiedPath = "content_modified.xml";
        const string outputPdfPath = "output.pdf";

        // Identifier of the TextSegment to update and the new text value
        const string targetId = "segment123";
        const string newText = "Updated text content";

        // Verify required files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML input not found: {xmlInputPath}");
            return;
        }

        // Load the XML representation of the PDF content
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlInputPath);

        // Prepare namespace manager (Aspose PDF XML may use a default namespace)
        XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
        nsMgr.AddNamespace("pdf", xmlDoc.DocumentElement.NamespaceURI);

        // Locate the TextSegment element with the specified Id attribute
        string xpath = $"//pdf:TextSegment[@Id='{targetId}']";
        XmlNode textSegmentNode = xmlDoc.SelectSingleNode(xpath, nsMgr);

        if (textSegmentNode == null)
        {
            Console.Error.WriteLine($"TextSegment with Id '{targetId}' not found.");
            return;
        }

        // Update the element's inner text
        textSegmentNode.InnerText = newText;

        // Save the modified XML to a temporary file
        xmlDoc.Save(xmlModifiedPath);

        // Bind the modified XML to the PDF template and save the result
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            pdfDoc.BindXml(xmlModifiedPath);
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated with updated TextSegment. Saved to '{outputPdfPath}'.");
    }
}