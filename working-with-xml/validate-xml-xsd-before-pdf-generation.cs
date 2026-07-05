using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;

class XmlToPdfGenerator
{
    // Path to the XML file to be converted
    private const string XmlFilePath = "input.xml";

    // Path to the XSD schema file for validation
    private const string XsdFilePath = "schema.xsd";

    // Path where the resulting PDF will be saved
    private const string OutputPdfPath = "output.pdf";

    static void Main()
    {
        // Ensure the XSD file exists before attempting validation
        if (!File.Exists(XsdFilePath))
        {
            Console.Error.WriteLine($"Schema file '{XsdFilePath}' not found. PDF generation aborted.");
            return;
        }

        // Validate the XML against the XSD schema first
        if (!ValidateXml(XmlFilePath, XsdFilePath))
        {
            Console.Error.WriteLine("XML validation failed. PDF generation aborted.");
            return;
        }

        // XML is valid – proceed with PDF generation
        GeneratePdfFromXml(XmlFilePath, OutputPdfPath);
        Console.WriteLine($"PDF successfully created at '{OutputPdfPath}'.");
    }

    /// <summary>
    /// Validates an XML file against an XSD schema.
    /// Returns true if validation succeeds; otherwise false.
    /// </summary>
    private static bool ValidateXml(string xmlPath, string xsdPath)
    {
        bool isValid = true;

        // Prepare the schema set
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdPath);

        // Configure XmlReaderSettings for schema validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            // Report warnings as well; you can add more flags if needed
            ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings |
                               XmlSchemaValidationFlags.ProcessIdentityConstraints
        };

        // Capture validation errors
        settings.ValidationEventHandler += (sender, args) =>
        {
            isValid = false;
            Console.Error.WriteLine($"Validation {args.Severity}: {args.Message}");
        };

        // Parse the XML with the configured settings
        using (XmlReader reader = XmlReader.Create(xmlPath, settings))
        {
            try
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }
            catch (XmlException ex)
            {
                isValid = false;
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
            }
            catch (IOException ex)
            {
                isValid = false;
                Console.Error.WriteLine($"IO error while reading XML: {ex.Message}");
            }
        }

        return isValid;
    }

    /// <summary>
    /// Loads the validated XML into an Aspose.Pdf Document and saves it as PDF.
    /// </summary>
    private static void GeneratePdfFromXml(string xmlPath, string pdfPath)
    {
        // Load options for XML → PDF conversion (no XSL transformation)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Use a using block to ensure the Document is disposed properly
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the document as PDF
            pdfDocument.Save(pdfPath);
        }
    }
}
