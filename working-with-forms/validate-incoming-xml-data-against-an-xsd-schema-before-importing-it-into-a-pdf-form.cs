using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf.Facades;

class Program
{
    // Validation event handler – collects validation errors
    private static void ValidationCallback(object sender, ValidationEventArgs args)
    {
        // Throw on any validation error or warning
        throw new XmlSchemaValidationException(args.Message, args.Exception);
    }

    static void Main()
    {
        const string inputPdfPath   = "form_template.pdf";   // existing PDF form
        const string xmlDataPath    = "data.xml";            // XML to import
        const string xsdSchemaPath  = "schema.xsd";          // XSD schema
        const string outputPdfPath  = "filled_form.pdf";     // result PDF

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF form not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data not found: {xmlDataPath}");
            return;
        }
        if (!File.Exists(xsdSchemaPath))
        {
            Console.Error.WriteLine($"XSD schema not found: {xsdSchemaPath}");
            return;
        }

        // ---------- XML validation against XSD ----------
        try
        {
            // Load the XSD schema
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, xsdSchemaPath);

            // Configure reader settings for validation
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = schemas,
                ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints |
                                  XmlSchemaValidationFlags.ReportValidationWarnings
            };
            settings.ValidationEventHandler += ValidationCallback;

            // Parse the XML with validation enabled
            using (FileStream xmlStream = File.OpenRead(xmlDataPath))
            using (XmlReader reader = XmlReader.Create(xmlStream, settings))
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }

            Console.WriteLine("XML validation succeeded.");
        }
        catch (XmlSchemaValidationException ex)
        {
            Console.Error.WriteLine($"XML validation failed: {ex.Message}");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error during validation: {ex.Message}");
            return;
        }

        // ---------- Import validated XML into PDF form ----------
        try
        {
            // Initialize the Form facade with the source PDF
            using (Form pdfForm = new Form(inputPdfPath))
            {
                // Import XML data (the XML has already been validated)
                using (FileStream xmlStream = File.OpenRead(xmlDataPath))
                {
                    pdfForm.ImportXml(xmlStream);
                }

                // Save the updated PDF to the output file
                pdfForm.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF form saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error importing XML into PDF: {ex.Message}");
        }
    }
}