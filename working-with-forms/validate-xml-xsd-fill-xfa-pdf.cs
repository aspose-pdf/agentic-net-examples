using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string xmlPath = "data.xml";
        const string xsdPath = "schema.xsd";
        const string outputPdf = "filled_form.pdf";

        // Verify required files exist
        if (!File.Exists(pdfPath) || !File.Exists(xmlPath) || !File.Exists(xsdPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load and validate the XML against the XSD schema
        XmlDocument xmlDoc = new XmlDocument();
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationEventHandler += (sender, args) =>
        {
            // Convert any validation warning/error into an exception
            throw new XmlSchemaValidationException(args.Message);
        };

        try
        {
            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                xmlDoc.Load(reader); // Validation occurs during load
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"XML validation failed: {ex.Message}");
            return;
        }

        // Load the PDF document, assign the validated XFA data, and save
        using (Document pdfDoc = new Document(pdfPath))
        {
            if (pdfDoc.Form.HasXfa)
            {
                pdfDoc.Form.AssignXfa(xmlDoc);
            }
            else
            {
                Console.Error.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported data saved to '{outputPdf}'.");
    }
}