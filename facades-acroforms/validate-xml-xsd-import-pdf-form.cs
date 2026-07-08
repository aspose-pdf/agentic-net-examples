using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "FormTemplate.pdf";   // source PDF with form fields
        const string pdfOutputPath   = "FormFilled.pdf";    // destination PDF after import
        const string xmlDataPath    = "data.xml";           // XML file containing form data
        const string xsdSchemaPath  = "data.xsd";           // XSD schema to validate XML against

        // Ensure the XSD schema file exists before attempting validation
        if (!File.Exists(xsdSchemaPath))
        {
            Console.Error.WriteLine($"Schema file not found: '{xsdSchemaPath}'. Validation cannot be performed.");
            return;
        }

        // Validate XML against XSD before importing
        if (!ValidateXml(xmlDataPath, xsdSchemaPath))
        {
            Console.Error.WriteLine("XML validation failed. Import aborted.");
            return;
        }

        // Import validated XML into the PDF form using the correct API
        using (Form form = new Form(pdfTemplatePath))
        {
            using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
            {
                // Correct method name: ImportXml
                form.ImportXml(xmlStream);
            }

            // Save the resulting PDF using the overload that takes the destination path
            form.Save(pdfOutputPath);
        }

        Console.WriteLine($"Form data imported successfully to '{pdfOutputPath}'.");
    }

    // Returns true if the XML file conforms to the XSD schema; otherwise false.
    static bool ValidateXml(string xmlPath, string xsdPath)
    {
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: '{xmlPath}'.");
            return false;
        }

        bool isValid = true;
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationEventHandler += (sender, args) =>
        {
            Console.Error.WriteLine($"Validation {args.Severity}: {args.Message}");
            isValid = false;
        };

        try
        {
            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception during XML validation: {ex.Message}");
            return false;
        }

        return isValid;
    }
}
