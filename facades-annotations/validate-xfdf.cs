using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xfdfPath = "form.xfdf";
        const string xsdPath = "xfdf.xsd";

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        if (!File.Exists(xsdPath))
        {
            Console.Error.WriteLine($"XSD schema file not found: {xsdPath}");
            return;
        }

        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
        settings.ValidationEventHandler += ValidationCallback;

        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        using (XmlReader reader = XmlReader.Create(xfdfStream, settings))
        {
            try
            {
                while (reader.Read())
                {
                    // Reading triggers validation via the settings.
                }
                Console.WriteLine("XFDF validation completed without errors.");
            }
            catch (XmlException xmlEx)
            {
                Console.Error.WriteLine($"XML parsing error: {xmlEx.Message}");
            }
        }
    }

    private static void ValidationCallback(object sender, ValidationEventArgs e)
    {
        string severity = e.Severity == XmlSeverityType.Error ? "Error" : "Warning";
        Console.Error.WriteLine($"{severity}: {e.Message}");
    }
}