using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Xml;
using System.Xml.Schema;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string schemaPath = "xmp_schema.xsd";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(schemaPath))
        {
            Console.Error.WriteLine($"XMP schema not found: {schemaPath}");
            return;
        }

        // Load the PDF and extract its XMP metadata
        using (Document doc = new Document(pdfPath))
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(doc);
            byte[] xmpData = xmp.GetXmpMetadata();

            // Validate the extracted XMP against the provided XSD schema
            bool isValid = ValidateXmlAgainstSchema(xmpData, schemaPath);
            Console.WriteLine(isValid ? "XMP metadata is valid." : "XMP metadata validation failed.");
        }
    }

    static bool ValidateXmlAgainstSchema(byte[] xmlBytes, string xsdPath)
    {
        bool valid = true;

        // Load the XSD schema
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdPath);

        // Configure XML reader settings for schema validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas
        };
        settings.ValidationEventHandler += (sender, args) =>
        {
            valid = false;
            Console.Error.WriteLine($"Validation {args.Severity}: {args.Message}");
        };

        // Parse and validate the XML
        using (MemoryStream ms = new MemoryStream(xmlBytes))
        using (XmlReader reader = XmlReader.Create(ms, settings))
        {
            try
            {
                while (reader.Read()) { }
            }
            catch (XmlException ex)
            {
                valid = false;
                Console.Error.WriteLine($"XML error: {ex.Message}");
            }
        }

        return valid;
    }
}