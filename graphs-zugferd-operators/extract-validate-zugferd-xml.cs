using System;
using System.IO;
using Aspose.Pdf;
using System.Xml;
using System.Xml.Schema;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string xsdPath = "ZUGFeRD-invoice.xsd";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xsdPath))
        {
            Console.Error.WriteLine($"XSD not found: {xsdPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Locate the ZUGFeRD XML attachment using FileSpecification (EmbeddedFile does not exist)
            FileSpecification zugFerdSpec = null;
            foreach (FileSpecification fs in pdfDoc.EmbeddedFiles)
            {
                if (fs.Name != null &&
                    fs.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) &&
                    fs.Name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    zugFerdSpec = fs;
                    break;
                }
            }

            if (zugFerdSpec == null)
            {
                Console.Error.WriteLine("ZUGFeRD XML attachment not found in PDF.");
                return;
            }

            // Extract the XML content to a memory stream
            using (MemoryStream xmlStream = new MemoryStream())
            {
                // The Contents property returns a Stream with the embedded file data
                zugFerdSpec.Contents.CopyTo(xmlStream);
                xmlStream.Position = 0;

                // Prepare XML schema validation settings
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add(null, xsdPath);

                XmlReaderSettings settings = new XmlReaderSettings
                {
                    Schemas = schemas,
                    ValidationType = ValidationType.Schema,
                    ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints
                };
                settings.ValidationEventHandler += ValidationCallback;

                // Validate the XML against the ZUGFeRD XSD
                using (XmlReader reader = XmlReader.Create(xmlStream, settings))
                {
                    try
                    {
                        while (reader.Read()) { }
                        Console.WriteLine("ZUGFeRD XML is valid against the XSD schema.");
                    }
                    catch (XmlSchemaValidationException ex)
                    {
                        Console.Error.WriteLine($"Validation error: {ex.Message}");
                    }
                }
            }
        }
    }

    // Callback to capture validation warnings and errors
    private static void ValidationCallback(object sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Error)
            Console.Error.WriteLine($"Error: {e.Message}");
        else
            Console.WriteLine($"Warning: {e.Message}");
    }
}
