using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath    = "template.pdf";
        const string xmlPath    = "data.xml";
        const string xsdPath    = "schema.xsd";
        const string outputPath = "filled.pdf";

        // Ensure required files exist
        if (!File.Exists(pdfPath) || !File.Exists(xmlPath) || !File.Exists(xsdPath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // ---------- XML validation ----------
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        // Load the XSD schema (no namespace specified)
        settings.Schemas.Add(null, xsdPath);
        // Capture validation errors
        settings.ValidationEventHandler += (sender, e) =>
        {
            Console.Error.WriteLine($"XML validation error: {e.Message}");
            // Abort on first error
            throw new XmlSchemaValidationException(e.Message);
        };

        try
        {
            // Parsing the XML triggers validation against the loaded schema
            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                while (reader.Read()) { }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"XML validation failed: {ex.Message}");
            return;
        }

        // ---------- Import XML into PDF form ----------
        using (Form form = new Form())
        {
            // Bind the PDF template that contains form fields
            form.BindPdf(pdfPath);

            // Import the validated XML data into the form fields using the correct API
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }

            // Save the resulting PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"Form data imported successfully. Output saved to '{outputPath}'.");
    }
}
