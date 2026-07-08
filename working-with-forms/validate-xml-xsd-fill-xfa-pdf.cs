using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Path to the PDF form template (must contain an XFA form)
    const string PdfTemplatePath = "form_template.pdf";

    // Paths for the XML data and its XSD schema
    const string XmlDataPath = "data.xml";
    const string XsdSchemaPath = "schema.xsd";

    // Output PDF after importing the validated XML
    const string OutputPdfPath = "filled_form.pdf";

    static void Main()
    {
        // Validate the XML against the XSD schema
        XmlDocument xmlDoc = LoadAndValidateXml(XmlDataPath, XsdSchemaPath);
        if (xmlDoc == null)
        {
            Console.Error.WriteLine("XML validation failed. Aborting.");
            return;
        }

        // Load the PDF form, assign the XFA data and save
        using (Document pdfDoc = new Document(PdfTemplatePath))
        {
            // Ensure the document actually contains an XFA form
            if (!pdfDoc.Form.HasXfa)
            {
                Console.Error.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Assign the validated XML to the XFA form
            pdfDoc.Form.AssignXfa(xmlDoc);

            // Save the resulting PDF
            pdfDoc.Save(OutputPdfPath);
        }

        Console.WriteLine($"PDF form saved to '{OutputPdfPath}'.");
    }

    /// <summary>
    /// Loads an XML file and validates it against the provided XSD schema.
    /// Returns the XmlDocument if validation succeeds; otherwise null.
    /// </summary>
    static XmlDocument LoadAndValidateXml(string xmlPath, string xsdPath)
    {
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return null;
        }

        if (!File.Exists(xsdPath))
        {
            Console.Error.WriteLine($"XSD schema file not found: {xsdPath}");
            return null;
        }

        // Prepare schema set
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdPath);

        // Configure XmlReaderSettings for validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            // Stop processing on first error
            ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
        };
        settings.ValidationEventHandler += (sender, args) =>
        {
            // Treat warnings as errors for strict validation
            if (args.Severity == XmlSeverityType.Error || args.Severity == XmlSeverityType.Warning)
                throw new XmlSchemaValidationException(args.Message);
        };

        try
        {
            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(reader); // Validation occurs during load
                return doc;
            }
        }
        catch (XmlSchemaValidationException ex)
        {
            Console.Error.WriteLine($"XML validation error: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error while loading XML: {ex.Message}");
            return null;
        }
    }
}