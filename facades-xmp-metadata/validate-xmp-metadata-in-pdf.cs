using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class XmpValidator
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // PDF to validate
        const string xmpSchemaPath  = "xmp_schema.xsd";    // Simple XMP XSD file (generated inline)
        const string validationLog  = "xmp_validation_log.txt";

        // ---------------------------------------------------------------------
        // 1. Ensure a PDF file exists – create a minimal one if it does not.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document seed = new Document())
            {
                // Add a single blank page – enough for Aspose to generate default XMP.
                seed.Pages.Add();
                seed.Save(inputPdfPath);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Ensure an XSD file exists – create a very permissive schema inline.
        //    The official XMP schema is large; for demo purposes a minimal schema
        //    that accepts any element is sufficient to show the validation flow.
        // ---------------------------------------------------------------------
        if (!File.Exists(xmpSchemaPath))
        {
            const string minimalXsd = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                                      "<xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" elementFormDefault=\"qualified\">\n" +
                                      "  <xs:element name=\"xmpmeta\">\n" +
                                      "    <xs:complexType>\n" +
                                      "      <xs:sequence>\n" +
                                      "        <xs:any namespace=\"##any\" processContents=\"skip\" minOccurs=\"0\" maxOccurs=\"unbounded\"/>\n" +
                                      "      </xs:sequence>\n" +
                                      "    </xs:complexType>\n" +
                                      "  </xs:element>\n" +
                                      "</xs:schema>";
            File.WriteAllText(xmpSchemaPath, minimalXsd);
        }

        // Ensure the log file starts empty
        File.WriteAllText(validationLog, string.Empty);

        // Load the PDF document (using block ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the PDF to the XMP metadata facade
            using (PdfXmpMetadata xmpMeta = new PdfXmpMetadata())
            {
                xmpMeta.BindPdf(pdfDoc);

                // Retrieve XMP metadata as a byte array
                byte[] xmpBytes = xmpMeta.GetXmpMetadata();

                // Load the XMP XML into an XmlDocument for validation
                using (MemoryStream xmpStream = new MemoryStream(xmpBytes))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmpStream);

                    // Prepare the XML schema set
                    XmlSchemaSet schemaSet = new XmlSchemaSet();
                    schemaSet.Add(null, xmpSchemaPath); // No target namespace specified

                    // Attach the schema to the document
                    xmlDoc.Schemas.Add(schemaSet);

                    bool isValid = true;

                    // Validation event handler records any errors to the log
                    ValidationEventHandler handler = (sender, args) =>
                    {
                        isValid = false;
                        File.AppendAllText(validationLog,
                            $"Error: {args.Message}{Environment.NewLine}");
                    };

                    // Perform validation
                    xmlDoc.Validate(handler);

                    // Write overall result to the log
                    string resultMessage = $"XMP validation result: {(isValid ? "Valid" : "Invalid")}{Environment.NewLine}";
                    File.AppendAllText(validationLog, resultMessage);
                }
            }
        }

        Console.WriteLine($"Validation completed. See '{validationLog}' for details.");
    }
}
