using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Path to the PDF form template
    const string PdfTemplatePath = "form_template.pdf";
    // Path to the XML data file
    const string XmlDataPath = "data.xml";
    // Path to the XSD schema file
    const string XsdSchemaPath = "schema.xsd";
    // Path for the filled PDF output
    const string OutputPdfPath = "filled_form.pdf";

    static void Main()
    {
        // Verify that required files exist
        if (!File.Exists(PdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {PdfTemplatePath}");
            return;
        }
        if (!File.Exists(XmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {XmlDataPath}");
            return;
        }
        if (!File.Exists(XsdSchemaPath))
        {
            Console.Error.WriteLine($"XSD schema file not found: {XsdSchemaPath}");
            return;
        }

        // Load and validate the XML document
        XmlDocument xmlDoc = new XmlDocument();
        bool isValid = ValidateXml(XmlDataPath, XsdSchemaPath, xmlDoc);
        if (!isValid)
        {
            Console.Error.WriteLine("XML validation failed. Aborting PDF form fill.");
            return;
        }

        // Ensure the XML has a root element before iterating
        if (xmlDoc.DocumentElement == null)
        {
            Console.Error.WriteLine("XML document does not contain a root element.");
            return;
        }

        // Open the PDF form inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(PdfTemplatePath))
        {
            // Iterate over each element in the XML and map it to a PDF form field
            // Assumes that XML element names match the PDF field names
            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                    continue;

                string fieldName = node.Name;
                string fieldValue = node.InnerText;

                // The Form indexer returns a WidgetAnnotation; cast it to Field before using.
                Field? pdfField = pdfDoc.Form[fieldName] as Field;
                if (pdfField != null)
                {
                    pdfField.Value = fieldValue;
                }
                else
                {
                    Console.WriteLine($"Warning: PDF does not contain field '{fieldName}'.");
                }
            }

            // Save the filled PDF
            pdfDoc.Save(OutputPdfPath);
        }

        Console.WriteLine($"PDF form successfully filled and saved to '{OutputPdfPath}'.");
    }

    // Validates an XML file against an XSD schema.
    // Returns true if validation succeeds; otherwise false.
    // The validated XmlDocument is output via the resultDoc parameter.
    static bool ValidateXml(string xmlPath, string xsdPath, XmlDocument resultDoc)
    {
        bool isValid = true;

        // Set up the schema set
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdPath);

        // Configure XmlReaderSettings for validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints |
                              XmlSchemaValidationFlags.ReportValidationWarnings
        };
        settings.ValidationEventHandler += (sender, args) =>
        {
            // Treat warnings as errors for strict validation
            Console.Error.WriteLine($"{args.Severity}: {args.Message}");
            isValid = false;
        };

        // Parse and validate the XML
        using (XmlReader reader = XmlReader.Create(xmlPath, settings))
        {
            try
            {
                resultDoc.Load(reader);
            }
            catch (XmlException ex)
            {
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
                return false;
            }
        }

        return isValid;
    }
}
