using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths and sample data
        string pdfPath = "sample.pdf";
        string xmlFileName = "ZUGFeRD-invoice.xml";
        string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Invoice><ID>12345</ID></Invoice>";
        string xsdContent = "<?xml version=\"1.0\" encoding=\"utf-8\"?><xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"><xs:element name=\"Invoice\"><xs:complexType><xs:sequence><xs:element name=\"ID\" type=\"xs:string\"/></xs:sequence></xs:complexType></xs:element></xs:schema>";

        // ---------------------------------------------------------------------
        // 1. Create a sample PDF and embed the ZUGFeRD XML as an attachment
        // ---------------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add();

            FileSpecification fileSpec = new FileSpecification();
            fileSpec.Name = xmlFileName;
            fileSpec.Description = "ZUGFeRD XML attachment";
            fileSpec.MIMEType = "application/xml";
            fileSpec.Contents = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent));

            pdfDoc.EmbeddedFiles.Add(fileSpec);
            pdfDoc.Save(pdfPath);
        }

        // ---------------------------------------------------------------------
        // 2. Open the PDF, locate the XML attachment and extract its content
        // ---------------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            FileSpecification zugFerdFile = null;
            foreach (FileSpecification fs in pdfDoc.EmbeddedFiles)
            {
                if (fs.Name != null && fs.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    zugFerdFile = fs;
                    break;
                }
            }

            if (zugFerdFile == null)
            {
                Console.WriteLine("No ZUGFeRD XML attachment found.");
                return;
            }

            string extractedXml;
            using (MemoryStream ms = new MemoryStream())
            {
                zugFerdFile.Contents.CopyTo(ms);
                extractedXml = Encoding.UTF8.GetString(ms.ToArray());
            }

            Console.WriteLine("Extracted ZUGFeRD XML:");
            Console.WriteLine(extractedXml);

            // -----------------------------------------------------------------
            // 3. Validate the extracted XML against the provided XSD schema
            // -----------------------------------------------------------------
            bool isValid = true;
            ValidationEventHandler eventHandler = (sender, e) =>
            {
                Console.WriteLine($"Validation {e.Severity}: {e.Message}");
                isValid = false;
            };

            XmlSchemaSet schemas = new XmlSchemaSet();
            using (StringReader xsdReader = new StringReader(xsdContent))
            {
                schemas.Add(null, XmlReader.Create(xsdReader));
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schemas;
            settings.ValidationEventHandler += eventHandler;

            using (StringReader xmlReader = new StringReader(extractedXml))
            using (XmlReader xmlValidReader = XmlReader.Create(xmlReader, settings))
            {
                while (xmlValidReader.Read())
                {
                    // Reading triggers validation
                }
            }

            Console.WriteLine(isValid ? "XML is valid against the XSD." : "XML validation failed.");
        }
    }
}
