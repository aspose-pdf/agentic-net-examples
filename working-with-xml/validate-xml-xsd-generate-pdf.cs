using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Validation callback – throws on first error
    private static void ValidationCallback(object sender, ValidationEventArgs args)
    {
        // Treat warnings as errors
        if (args.Severity == XmlSeverityType.Error || args.Severity == XmlSeverityType.Warning)
        {
            throw new XmlSchemaValidationException($"XML validation error: {args.Message}", args.Exception);
        }
    }

    static void Main()
    {
        const string xmlFilePath      = "input.xml";      // source XML
        const string xsdFilePath      = "schema.xsd";     // XSD schema
        const string outputPdfPath    = "output.pdf";     // generated PDF

        // Ensure files exist
        if (!File.Exists(xmlFilePath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFilePath}");
            return;
        }
        if (!File.Exists(xsdFilePath))
        {
            Console.Error.WriteLine($"XSD file not found: {xsdFilePath}");
            return;
        }

        // 1. Validate XML against XSD
        try
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(null, xsdFilePath);
            settings.ValidationEventHandler += ValidationCallback;

            using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
            {
                // Parse the entire document – validation occurs during read
                while (reader.Read()) { }
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

        // 2. Load validated XML into Aspose.Pdf Document
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions(); // no XSL transformation
        using (Document pdfDocument = new Document(xmlFilePath, xmlLoadOptions))
        {
            // Optional: add a title using DocumentInfo
            pdfDocument.Info.Title = Path.GetFileNameWithoutExtension(outputPdfPath);

            // 3. Save as PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{outputPdfPath}'.");
    }
}
