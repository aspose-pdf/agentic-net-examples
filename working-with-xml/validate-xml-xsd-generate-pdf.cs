using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;

class Program
{
    // Validates an XML file against an XSD schema.
    // Returns true if the XML is valid; otherwise false.
    // Any validation errors are collected in the out parameter 'log'.
    static bool ValidateXml(string xmlPath, string xsdPath, out string log)
    {
        StringBuilder sb = new StringBuilder();
        bool isValid = true;

        // Set up the XML schema set.
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdPath);

        // Configure reader settings for validation.
        XmlReaderSettings settings = new XmlReaderSettings {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            // Continue validation to collect all errors.
            ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints |
                              XmlSchemaValidationFlags.ReportValidationWarnings
        };

        // Event handler captures validation errors.
        settings.ValidationEventHandler += (sender, args) =>
        {
            isValid = false;
            sb.AppendLine($"{args.Severity}: {args.Message}");
        };

        // Parse the XML with the configured settings.
        using (var reader = XmlReader.Create(xmlPath, settings))
        {
            try
            {
                while (reader.Read()) { /* just iterate */ }
            }
            catch (XmlException ex)
            {
                isValid = false;
                sb.AppendLine($"XML Exception: {ex.Message}");
            }
        }

        log = sb.ToString();
        return isValid;
    }

    static void Main()
    {
        const string xmlFile   = "input.xml";   // XML source
        const string xsdFile   = "schema.xsd";  // Corresponding XSD
        const string pdfOutput = "output.pdf";  // Resulting PDF

        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }

        if (!File.Exists(xsdFile))
        {
            Console.Error.WriteLine($"XSD file not found: {xsdFile}");
            return;
        }

        // Step 1: Validate XML against XSD.
        if (!ValidateXml(xmlFile, xsdFile, out string validationLog))
        {
            Console.Error.WriteLine("XML validation failed. Errors:");
            Console.Error.WriteLine(validationLog);
            return;
        }

        Console.WriteLine("XML validation succeeded.");

        // Step 2: Load the validated XML into a PDF document.
        // XmlLoadOptions can be used without an XSL transformation.
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();

        using (Document pdfDoc = new Document(xmlFile, xmlLoadOptions))
        {
            // Step 3: Save the PDF.
            pdfDoc.Save(pdfOutput);
        }

        Console.WriteLine($"PDF generated successfully: {pdfOutput}");
    }
}