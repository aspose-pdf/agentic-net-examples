using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "PdfForm.pdf";
        const string xmlDataPath = "data.xml";
        const string xsdPath = "schema.xsd";
        const string outputPdfPath = "output.pdf";

        // Resolve paths relative to the executable directory to avoid FileNotFoundException
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string pdfPath = Path.Combine(baseDir, pdfTemplatePath);
        string xmlPath = Path.Combine(baseDir, xmlDataPath);
        string schemaPath = Path.Combine(baseDir, xsdPath);
        string outPath = Path.Combine(baseDir, outputPdfPath);

        // Ensure the XSD schema file exists before attempting validation
        if (!File.Exists(schemaPath))
        {
            Console.Error.WriteLine($"Schema file not found: '{schemaPath}'. Validation cannot be performed.");
            return;
        }

        // Validate the XML file against the XSD schema
        bool isValid = ValidateXml(xmlPath, schemaPath);
        if (!isValid)
        {
            Console.Error.WriteLine("XML validation failed. Import aborted.");
            return;
        }

        // Import the validated XML into the PDF form using the non‑obsolete API
        Form form = new Form(pdfPath);
        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        {
            form.ImportXml(xmlStream);
        }
        // Save to the desired output file using the overload that accepts a destination path.
        form.Save(outPath);
        Console.WriteLine($"Form data imported successfully to '{outPath}'.");
    }

    private static bool ValidateXml(string xmlPath, string xsdPath)
    {
        bool valid = true;
        XmlSchemaSet schemaSet = new XmlSchemaSet();
        schemaSet.Add(null, xsdPath);

        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemaSet
        };
        settings.ValidationEventHandler += (sender, e) =>
        {
            valid = false;
            Console.Error.WriteLine($"Validation {e.Severity}: {e.Message}");
        };

        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (XmlReader reader = XmlReader.Create(xmlStream, settings))
        {
            while (reader.Read())
            {
                // Reading triggers validation events
            }
        }

        return valid;
    }
}
