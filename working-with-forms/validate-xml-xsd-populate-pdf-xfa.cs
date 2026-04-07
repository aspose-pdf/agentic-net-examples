using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Validation event handler – throws on any validation error
    static void ValidationCallback(object sender, ValidationEventArgs args)
    {
        // Treat warnings as errors
        if (args.Severity == XmlSeverityType.Error || args.Severity == XmlSeverityType.Warning)
            throw new XmlSchemaValidationException(args.Message);
    }

    static void Main()
    {
        const string pdfFormPath   = "form_template.pdf";   // existing PDF with XFA form
        const string xmlDataPath   = "data.xml";            // XML to import
        const string xsdSchemaPath = "schema.xsd";          // XSD schema
        const string outputPdfPath = "filled_form.pdf";

        // Ensure files exist
        if (!File.Exists(pdfFormPath) || !File.Exists(xmlDataPath) || !File.Exists(xsdSchemaPath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // 1. Validate XML against XSD
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ValidationType = ValidationType.Schema;
        settings.Schemas.Add(null, xsdSchemaPath);
        settings.ValidationEventHandler += ValidationCallback;

        try
        {
            using (XmlReader reader = XmlReader.Create(xmlDataPath, settings))
            {
                // Parse the entire document – validation occurs during read
                while (reader.Read()) { }
            }
        }
        catch (XmlSchemaValidationException ex)
        {
            Console.Error.WriteLine($"XML validation failed: {ex.Message}");
            return;
        }

        // 2. Load validated XML into XmlDocument
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlDataPath);

        // 3. Load the PDF form, assign XFA data, and save
        using (Document pdfDoc = new Document(pdfFormPath))
        {
            // Assign the XFA XML to the form
            pdfDoc.Form.AssignXfa(xmlDoc);

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF form saved to '{outputPdfPath}'.");
    }
}