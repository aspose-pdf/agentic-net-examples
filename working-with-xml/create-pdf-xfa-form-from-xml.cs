using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (can be empty), the XML definition and the output PDF
        const string sourcePdfPath = "template.pdf";   // existing PDF or can be empty
        const string xmlDefinitionPath = "formDefinition.xml";
        const string outputPdfPath = "filled_form.pdf";

        // Ensure the XML definition file exists
        if (!File.Exists(xmlDefinitionPath))
        {
            Console.Error.WriteLine($"XML definition not found: {xmlDefinitionPath}");
            return;
        }

        // Load (or create) the PDF document inside a using block for deterministic disposal
        using (Document doc = File.Exists(sourcePdfPath)
                               ? new Document(sourcePdfPath)
                               : new Document())
        {
            // If the document has no pages, add a blank page (required for XFA assignment)
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Load the XML that defines the XFA form fields
            XmlDocument xfaXml = new XmlDocument();
            xfaXml.Load(xmlDefinitionPath);

            // -----------------------------------------------------------------
            // Set default values for specific fields directly in the XFA XML.
            // The exact XPath depends on the structure of your XFA definition.
            // Adjust the XPath expressions if your field hierarchy differs.
            // -----------------------------------------------------------------
            SetXfaFieldValue(xfaXml, "//form1/firstName", "John");
            SetXfaFieldValue(xfaXml, "//form1/lastName",  "Doe");
            SetXfaFieldValue(xfaXml, "//form1/age",       "30");

            // Assign the (now modified) XFA definition to the document's form
            doc.Form.AssignXfa(xfaXml);

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with XFA form fields saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Helper that sets the inner text of the first node matching the supplied XPath.
    /// If the node does not exist, the method does nothing (you may extend it to create nodes).
    /// </summary>
    private static void SetXfaFieldValue(XmlDocument doc, string xpath, string value)
    {
        XmlNode node = doc.SelectSingleNode(xpath);
        if (node != null)
        {
            node.InnerText = value;
        }
    }
}
