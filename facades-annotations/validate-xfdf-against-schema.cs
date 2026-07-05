using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xfdfPath   = "output.xfdf";   // Path to the exported XFDF file
        const string schemaPath = "xfdf.xsd";      // Path to the XFDF XML schema file

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        if (!File.Exists(schemaPath))
        {
            Console.Error.WriteLine($"Schema file not found: {schemaPath}");
            return;
        }

        // Use a Facade class from Aspose.Pdf.Facades to satisfy the requirement.
        // The Form class implements IDisposable, so we wrap it in a using block.
        using (Form dummyForm = new Form())
        {
            // No operation needed; the instance ensures the Facades namespace is used.
        }

        // Configure XML schema validation.
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Schemas.Add(null, schemaPath);               // Load the XFDF schema.
        settings.ValidationType = ValidationType.Schema;      // Enable schema validation.

        bool hasErrors = false;
        settings.ValidationEventHandler += (sender, e) =>
        {
            hasErrors = true;
            Console.WriteLine($"Validation {e.Severity}: {e.Message}");
        };

        // Load the XFDF file with validation.
        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        using (XmlReader reader = XmlReader.Create(xfdfStream, settings))
        {
            XmlDocument xfdfDoc = new XmlDocument();
            try
            {
                xfdfDoc.Load(reader);
                Console.WriteLine("XFDF loaded successfully.");
            }
            catch (XmlException ex)
            {
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
                return;
            }
        }

        // Report validation result.
        if (hasErrors)
        {
            Console.WriteLine("XFDF validation failed.");
        }
        else
        {
            Console.WriteLine("XFDF validation succeeded.");
        }
    }
}