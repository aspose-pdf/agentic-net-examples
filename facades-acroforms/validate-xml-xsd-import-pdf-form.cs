using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfFormPath   = "FormTemplate.pdf";   // existing PDF form
        const string xmlDataPath   = "data.xml";           // XML to import
        const string xsdSchemaPath = "schema.xsd";         // XSD schema
        const string outputPdfPath = "FormFilled.pdf";     // result PDF

        // Validate XML against XSD
        if (!ValidateXml(xmlDataPath, xsdSchemaPath, out string validationError))
        {
            Console.Error.WriteLine($"XML validation failed: {validationError}");
            return;
        }

        // Import validated XML into the PDF form
        using (Form form = new Form())
        {
            // Bind the source PDF form
            form.BindPdf(pdfFormPath);

            // Import XML data
            using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }

            // Save the filled PDF
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form successfully filled and saved to '{outputPdfPath}'.");
    }

    // Returns true if XML is valid; otherwise false with an error message.
    private static bool ValidateXml(string xmlPath, string xsdPath, out string errorMessage)
    {
        // Local variable to capture validation errors (cannot capture 'out' directly in a lambda)
        string localError = string.Empty;
        errorMessage = string.Empty;
        try
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, xsdPath);

            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = schemas,
                // Stop on first error (warnings are also reported)
                ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
            };
            settings.ValidationEventHandler += (sender, args) =>
            {
                // Capture the first validation error or warning
                if (string.IsNullOrEmpty(localError))
                {
                    localError = args.Message;
                }
            };

            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                while (reader.Read()) { } // Parse the entire document
            }

            // Propagate the captured error (if any) to the out parameter
            errorMessage = localError;
            // Validation succeeded when no error was captured
            return string.IsNullOrEmpty(localError);
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return false;
        }
    }
}
