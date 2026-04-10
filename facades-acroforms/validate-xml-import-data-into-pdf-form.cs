using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplate = "PdfForm.pdf";      // source PDF with form fields
        const string outputPdf   = "PdfForm_Imported.pdf"; // destination PDF
        const string xmlData    = "import.xml";       // XML containing field values
        const string xsdPath    = "schema.xsd";       // XSD schema for validation

        // Ensure required files exist
        if (!File.Exists(pdfTemplate) || !File.Exists(xmlData) || !File.Exists(xsdPath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Validate the XML against the XSD schema
        if (!ValidateXml(xmlData, xsdPath))
        {
            Console.Error.WriteLine("XML validation failed. Import aborted.");
            return;
        }

        // Import the validated XML into the PDF form
        using (Form form = new Form(pdfTemplate, outputPdf))
        {
            using (FileStream xmlStream = new FileStream(xmlData, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }
            // Save writes to the output file specified in the constructor
            form.Save();
        }

        Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdf}'.");
    }

    // Returns true if the XML conforms to the XSD; otherwise false.
    static bool ValidateXml(string xmlPath, string xsdPath)
    {
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdPath);

        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
        };

        bool isValid = true;
        settings.ValidationEventHandler += (sender, e) =>
        {
            Console.Error.WriteLine($"Validation {e.Severity}: {e.Message}");
            if (e.Severity == XmlSeverityType.Error)
                isValid = false;
        };

        try
        {
            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                while (reader.Read()) { }
            }
        }
        catch (XmlException ex)
        {
            Console.Error.WriteLine($"XML parsing error: {ex.Message}");
            return false;
        }

        return isValid;
    }
}