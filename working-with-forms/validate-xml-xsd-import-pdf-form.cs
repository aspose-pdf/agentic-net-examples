using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Validation flag set by the event handler
    private static bool _isXmlValid = true;

    static void Main()
    {
        const string xmlPath      = "data.xml";      // Incoming XML data
        const string xsdPath      = "schema.xsd";    // XSD schema to validate against
        const string pdfPath      = "template.pdf";  // PDF form template
        const string outputPdf    = "filled_form.pdf";

        // Ensure files exist
        if (!File.Exists(xmlPath) || !File.Exists(xsdPath) || !File.Exists(pdfPath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // 1. Validate XML against XSD
        if (!ValidateXml(xmlPath, xsdPath))
        {
            Console.Error.WriteLine("XML validation failed. Import aborted.");
            return;
        }

        // 2. Load PDF document (lifecycle: create & load)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // 3. Load XML into an XmlDocument (required by AssignXfa)
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // 4. Import XML data into the PDF form (XFA)
            // AssignXfa sets the XFA data of the form to the provided XML
            pdfDocument.Form.AssignXfa(xmlDoc);

            // 5. Save the updated PDF (lifecycle: save)
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"PDF form saved to '{outputPdf}'.");
    }

    // Performs XML validation using .NET's XmlReader with XSD schema
    private static bool ValidateXml(string xmlFile, string xsdFile)
    {
        _isXmlValid = true; // reset flag

        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ValidationType = ValidationType.Schema;
        settings.Schemas.Add(null, xsdFile);
        settings.ValidationEventHandler += ValidationEventHandler;

        using (XmlReader reader = XmlReader.Create(xmlFile, settings))
        {
            try
            {
                while (reader.Read()) { /* just iterate to trigger validation */ }
            }
            catch (XmlException ex)
            {
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
                _isXmlValid = false;
            }
        }

        return _isXmlValid;
    }

    // Handles validation warnings and errors
    private static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
        Console.Error.WriteLine($"Validation {e.Severity}: {e.Message}");
        if (e.Severity == XmlSeverityType.Error)
            _isXmlValid = false;
    }
}