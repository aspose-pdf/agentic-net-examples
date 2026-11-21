using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string xmlPath = "data.xml";
        const string xsdPath = "schema.xsd";
        const string outputPath = "filled_form.pdf";

        // Verify that required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsdPath))
        {
            Console.Error.WriteLine($"XSD not found: {xsdPath}");
            return;
        }

        // -------------------------------------------------
        // Validate the XML file against the XSD schema
        // -------------------------------------------------
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdPath);

        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas
        };
        settings.ValidationEventHandler += ValidationCallback;

        bool isValid = true;
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
            isValid = false;
        }
        catch (XmlSchemaValidationException)
        {
            // ValidationCallback already reported the error
            isValid = false;
        }

        if (!isValid)
        {
            Console.Error.WriteLine("XML validation failed. Import aborted.");
            return;
        }

        // -------------------------------------------------
        // Import the validated XML data into the PDF form
        // -------------------------------------------------
        using (Form form = new Form())
        {
            form.BindPdf(pdfPath); // Load the PDF form

            // ImportXml expects a Stream, so open the XML file as a FileStream
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }

            form.Save(outputPath); // Save the filled PDF
        }

        Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
    }

    // Called for each validation error or warning
    static void ValidationCallback(object sender, ValidationEventArgs e)
    {
        Console.Error.WriteLine($"Validation {e.Severity}: {e.Message}");
        if (e.Severity == XmlSeverityType.Error)
        {
            // Throw to stop validation loop and mark the XML as invalid
            throw new XmlSchemaValidationException(e.Message);
        }
    }
}
