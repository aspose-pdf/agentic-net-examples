using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Validation event handler – throws on any validation error
    private static void ValidationCallback(object sender, ValidationEventArgs args)
    {
        // Treat warnings as errors for strict validation
        if (args.Severity == XmlSeverityType.Error || args.Severity == XmlSeverityType.Warning)
        {
            throw new XmlSchemaValidationException($"XML validation error: {args.Message}", args.Exception);
        }
    }

    static void Main()
    {
        const string xmlFilePath      = "data.xml";      // incoming XML data
        const string xsdFilePath      = "schema.xsd";    // XSD schema to validate against
        const string pdfTemplatePath  = "form_template.pdf"; // PDF with XFA form
        const string outputPdfPath    = "filled_form.pdf";

        // Ensure required files exist
        if (!File.Exists(xmlFilePath) || !File.Exists(xsdFilePath) || !File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Load and compile the XSD schema
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdFilePath);

        // Prepare XML reader settings for validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints |
                              XmlSchemaValidationFlags.ReportValidationWarnings
        };
        settings.ValidationEventHandler += ValidationCallback;

        // Load XML into XmlDocument while performing validation
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
            {
                xmlDoc.Load(reader); // Validation occurs during load
            }
        }
        catch (XmlSchemaValidationException ex)
        {
            Console.Error.WriteLine($"XML validation failed: {ex.Message}");
            return;
        }

        // At this point XML is valid – import it into the PDF form (XFA)
        try
        {
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // Assign the validated XML as XFA data
                pdfDoc.Form.AssignXfa(xmlDoc);

                // Save the resulting PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF form saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}