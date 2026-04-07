using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class ExportTaxValue
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "TaxForm.pdf";
        const string outputXmlPath  = "TaxValue.xml";      // custom XML with only the tax value
        const string pdfXmlPath     = "TaxForm_Model.xml"; // full PDF model exported by Aspose.Pdf

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (document‑disposal‑with‑using rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Retrieve the calculated tax field value after form submission.
            //    Use the Form field's Value property (not WidgetAnnotation.Value).
            // -----------------------------------------------------------------
            const string taxFieldName = "Tax"; // adjust to the actual field name in the PDF
            string taxValue = string.Empty;

            if (pdfDoc.Form.HasField(taxFieldName))
            {
                // The indexer returns a Field (e.g., TextBoxField). Use its Value property.
                Field taxField = pdfDoc.Form[taxFieldName] as Field;
                taxValue = taxField?.Value?.ToString() ?? string.Empty;
            }
            else
            {
                Console.Error.WriteLine($"Field '{taxFieldName}' not found in the form.");
            }

            // -----------------------------------------------------------------
            // 2. Create a simple XML document containing only the tax value.
            // -----------------------------------------------------------------
            XmlDocument customXml = new XmlDocument();

            // Create root element <FormData>
            XmlElement root = customXml.CreateElement("FormData");
            customXml.AppendChild(root);

            // Add <Tax> element with the extracted value
            XmlElement taxElement = customXml.CreateElement("Tax");
            taxElement.InnerText = taxValue;
            root.AppendChild(taxElement);

            // Save the custom XML file
            customXml.Save(outputXmlPath);
            Console.WriteLine($"Custom tax XML saved to '{outputXmlPath}'.");

            // -----------------------------------------------------------------
            // 3. (Optional) Export the entire PDF document model to XML.
            //    According to the save‑to‑non‑pdf‑always‑use‑save‑options rule,
            //    XmlSaveOptions must be supplied.
            // -----------------------------------------------------------------
            XmlSaveOptions xmlOpts = new XmlSaveOptions();
            pdfDoc.Save(pdfXmlPath, xmlOpts);
            Console.WriteLine($"Full PDF model XML saved to '{pdfXmlPath}'.");
        }
    }
}
