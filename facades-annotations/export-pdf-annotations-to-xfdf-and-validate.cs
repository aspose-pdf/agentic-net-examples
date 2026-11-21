using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf.Facades; // required by task

class Program
{
    static void Main()
    {
        const string xfdfPath   = "exported.xfdf";   // path to the XFDF file to validate
        const string schemaPath = "xfdf.xsd";        // path to the XFDF schema file

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        if (!File.Exists(schemaPath))
        {
            Console.Error.WriteLine($"XFDF schema file not found: {schemaPath}");
            return;
        }

        // Prepare XML schema set
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, schemaPath); // namespace is empty for XFDF

        // Configure validation settings
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints |
                              XmlSchemaValidationFlags.ReportValidationWarnings
        };

        // Capture validation errors
        bool hasErrors = false;
        settings.ValidationEventHandler += (sender, args) =>
        {
            hasErrors = true;
            Console.Error.WriteLine($"Validation {args.Severity}: {args.Message}");
        };

        // Load and validate the XFDF file
        using (XmlReader reader = XmlReader.Create(xfdfPath, settings))
        {
            try
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }
            catch (XmlException ex)
            {
                hasErrors = true;
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
            }
        }

        if (!hasErrors)
        {
            Console.WriteLine("XFDF validation succeeded – no schema errors detected.");
        }
        else
        {
            Console.WriteLine("XFDF validation failed – see errors above.");
        }
    }
}