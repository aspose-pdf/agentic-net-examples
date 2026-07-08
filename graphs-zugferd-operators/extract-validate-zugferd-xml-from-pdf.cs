using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf; // Core PDF API only

class ZugferdExtractor
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";          // Input PDF containing ZUGFeRD data
        const string outputXmlPath = "zugferd.xml";    // Extracted XML file
        const string xsdPath = "ZUGFeRD1p0.xsd";       // Path to the official ZUGFeRD XSD schema

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xsdPath))
        {
            Console.Error.WriteLine($"XSD schema not found: {xsdPath}");
            return;
        }

        try
        {
            // Load the PDF document (deterministic disposal with using)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // ------------------------------------------------------------
                // Extract the embedded ZUGFeRD XML using reflection.
                // The core Aspose.Pdf API does not expose a concrete
                // EmbeddedFile type, so we inspect the objects in the
                // EmbeddedFiles collection at runtime.
                // ------------------------------------------------------------
                object xmlEmbedded = null;
                foreach (var embedded in pdfDoc.EmbeddedFiles)
                {
                    var nameProp = embedded.GetType().GetProperty("Name");
                    if (nameProp != null)
                    {
                        var name = nameProp.GetValue(embedded) as string;
                        if (!string.IsNullOrEmpty(name) &&
                            name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            xmlEmbedded = embedded;
                            break;
                        }
                    }
                }

                if (xmlEmbedded == null)
                {
                    Console.Error.WriteLine("No embedded XML (ZUGFeRD) file found in the PDF.");
                    return;
                }

                // Try to call the Save(string) method directly.
                var saveMethod = xmlEmbedded.GetType().GetMethod("Save", new[] { typeof(string) });
                if (saveMethod != null)
                {
                    saveMethod.Invoke(xmlEmbedded, new object[] { outputXmlPath });
                }
                else
                {
                    // Fallback: obtain the raw stream from the FileSpecification.
                    var fileSpecProp = xmlEmbedded.GetType().GetProperty("FileSpecification");
                    var fileSpec = fileSpecProp?.GetValue(xmlEmbedded);
                    var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                    var contents = contentsProp?.GetValue(fileSpec) as Stream;
                    if (contents != null)
                    {
                        using (var outStream = File.Create(outputXmlPath))
                        {
                            contents.CopyTo(outStream);
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("Unable to extract XML content from the embedded file.");
                        return;
                    }
                }

                Console.WriteLine($"ZUGFeRD XML extracted to: {outputXmlPath}");

                // Validate the extracted XML against the provided XSD schema.
                bool isValid = ValidateXmlAgainstXsd(outputXmlPath, xsdPath);
                Console.WriteLine(isValid ? "XML validation succeeded." : "XML validation failed. See errors above.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Validates an XML file against an XSD schema.
    // Returns true if validation succeeds; otherwise false.
    static bool ValidateXmlAgainstXsd(string xmlFilePath, string xsdFilePath)
    {
        bool isValid = true;

        // Set up the schema set.
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdFilePath);

        // Configure XML reader settings for validation.
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
        };
        settings.ValidationEventHandler += (sender, args) =>
        {
            // Treat warnings as errors for strict validation.
            isValid = false;
            Console.Error.WriteLine($"Validation {args.Severity}: {args.Message}");
        };

        // Parse and validate the XML.
        using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
        {
            try
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }
            catch (XmlException xe)
            {
                isValid = false;
                Console.Error.WriteLine($"XML parsing error: {xe.Message}");
            }
        }

        return isValid;
    }
}
