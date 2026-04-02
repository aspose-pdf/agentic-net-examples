using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Resolve the base directory of the executing assembly
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        // Define the data folder relative to the base directory
        string dataDir = Path.Combine(baseDir, "Data");
        // Ensure the folder exists (creates it if missing)
        Directory.CreateDirectory(dataDir);

        // Build full paths for the XML and XSD files
        string xmlPath = Path.Combine(dataDir, "sample.xml");
        string xsdPath = Path.Combine(dataDir, "sample.xsd");
        string pdfPath = Path.Combine(baseDir, "output.pdf");

        // Verify that the source files exist before proceeding
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsdPath))
        {
            Console.WriteLine($"XSD schema not found: {xsdPath}");
            return;
        }

        // Validate the XML against the XSD schema
        bool isValid = ValidateXml(xmlPath, xsdPath);
        if (!isValid)
        {
            Console.WriteLine("XML validation failed. PDF generation aborted.");
            return;
        }

        // Load the validated XML and convert it to PDF
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully at: {pdfPath}");
    }

    static bool ValidateXml(string xmlFile, string xsdFile)
    {
        bool valid = true;
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdFile);

        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas
        };
        settings.ValidationEventHandler += (sender, e) =>
        {
            Console.WriteLine($"Validation {e.Severity}: {e.Message}");
            valid = false;
        };

        using (XmlReader reader = XmlReader.Create(xmlFile, settings))
        {
            while (reader.Read())
            {
                // Reading triggers validation events
            }
        }

        return valid;
    }
}
