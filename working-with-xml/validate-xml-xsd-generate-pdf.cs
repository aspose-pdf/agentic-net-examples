using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Paths for the sample files
    private const string XmlFilePath = "input.xml";
    private const string XsdFilePath = "schema.xsd";
    private const string PdfOutputPath = "output.pdf";

    static void Main()
    {
        // Ensure sample XML and XSD exist so the demo runs in an empty sandbox
        CreateSampleFiles();

        // Validate the XML against the XSD schema
        if (!ValidateXml(XmlFilePath, XsdFilePath))
        {
            Console.Error.WriteLine("XML validation failed. PDF generation aborted.");
            return;
        }

        // XML is valid – generate a PDF from its content
        GeneratePdfFromXml(XmlFilePath, PdfOutputPath);

        Console.WriteLine($"PDF successfully generated at '{PdfOutputPath}'.");
    }

    /// <summary>
    /// Creates minimal sample XML and XSD files if they are missing.
    /// This makes the example self‑contained for the sandbox environment.
    /// </summary>
    private static void CreateSampleFiles()
    {
        if (!File.Exists(XmlFilePath))
        {
            string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<root>\n  <message>Hello, World!</message>\n</root>";
            File.WriteAllText(XmlFilePath, xmlContent);
        }

        if (!File.Exists(XsdFilePath))
        {
            string xsdContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">\n  <xs:element name=\"root\">\n    <xs:complexType>\n      <xs:sequence>\n        <xs:element name=\"message\" type=\"xs:string\"/>\n      </xs:sequence>\n    </xs:complexType>\n  </xs:element>\n</xs:schema>";
            File.WriteAllText(XsdFilePath, xsdContent);
        }
    }

    /// <summary>
    /// Validates an XML file against an XSD schema.
    /// Returns true if the XML is valid; otherwise false.
    /// </summary>
    private static bool ValidateXml(string xmlPath, string xsdPath)
    {
        bool isValid = true;

        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ValidationType = ValidationType.Schema;
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationEventHandler += (sender, args) =>
        {
            isValid = false;
            Console.Error.WriteLine($"Validation {args.Severity}: {args.Message}");
        };

        using (XmlReader reader = XmlReader.Create(xmlPath, settings))
        {
            try
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }
            catch (XmlException ex)
            {
                isValid = false;
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
            }
        }

        return isValid;
    }

    /// <summary>
    /// Generates a simple PDF using Aspose.Pdf based on the content of the validated XML.
    /// The example extracts the <message> element and writes its text to the first page.
    /// </summary>
    private static void GeneratePdfFromXml(string xmlPath, string pdfPath)
    {
        // Load the XML document
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Extract the text inside <message>
        var messageNode = xmlDoc.SelectSingleNode("/root/message");
        string message = messageNode?.InnerText ?? "(no message)";

        // Create a new PDF document
        using (var pdfDoc = new Document())
        {
            // Add a page
            var page = pdfDoc.Pages.Add();

            // Add the extracted message as a text fragment
            var textFragment = new TextFragment(message)
            {
                // Optional styling
                Position = new Position(100, 700), // X, Y coordinates (points)
                TextState = { FontSize = 14, Font = FontRepository.FindFont("Arial") }
            };
            page.Paragraphs.Add(textFragment);

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }
    }
}
