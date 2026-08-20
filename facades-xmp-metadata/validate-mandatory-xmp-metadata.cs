using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfPublicationValidator
{
    // Validates that all mandatory XMP fields are present in the PDF.
    // If validation succeeds, the PDF is saved to the specified output path.
    // Otherwise, an exception is thrown.
    public static void ValidateAndPublish(string inputPdfPath, string[] mandatoryXmpFields, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // Load the PDF document (lifecycle: create & load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the PDF to the XMP metadata facade
            using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata())
            {
                xmpFacade.BindPdf(pdfDoc);

                // Retrieve the full XMP metadata as XML bytes
                byte[] xmpBytes = xmpFacade.GetXmpMetadata();
                if (xmpBytes == null || xmpBytes.Length == 0)
                    throw new InvalidOperationException("The PDF does not contain any XMP metadata.");

                // Load the XML into an XmlDocument for querying
                XmlDocument xmlDoc = new XmlDocument();
                using (MemoryStream ms = new MemoryStream(xmpBytes))
                {
                    xmlDoc.Load(ms);
                }

                // Namespace manager to handle common XMP namespaces (dc, xmp, etc.)
                XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
                nsMgr.AddNamespace("xmp", "http://ns.adobe.com/xap/1.0/");
                nsMgr.AddNamespace("pdf", "http://ns.adobe.com/pdf/1.3/");
                nsMgr.AddNamespace("pdfa", "http://www.aiim.org/pdfa/ns/schema#");
                nsMgr.AddNamespace("pdfaid", "http://www.aiim.org/pdfa/ns/id/");

                // Verify each mandatory field exists and has a non‑empty value
                foreach (string field in mandatoryXmpFields)
                {
                    // The field name may include a prefix (e.g., "dc:creator").
                    // Split prefix and local name to build an XPath expression.
                    string[] parts = field.Split(':');
                    if (parts.Length != 2)
                        throw new ArgumentException($"Invalid XMP field format: {field}. Expected prefix:localName.");

                    string prefix = parts[0];
                    string localName = parts[1];

                    // Build XPath: //prefix:localName
                    string xpath = $"//{prefix}:{localName}";
                    XmlNode node = xmlDoc.SelectSingleNode(xpath, nsMgr);

                    if (node == null || string.IsNullOrWhiteSpace(node.InnerText))
                        throw new InvalidOperationException($"Mandatory XMP field '{field}' is missing or empty.");
                }
            }

            // All mandatory fields are present – save (publish) the PDF.
            // Save operation follows the lifecycle rule (using block ensures disposal).
            pdfDoc.Save(outputPdfPath);
        }
    }

    // Example usage
    static void Main()
    {
        string inputPath = "source.pdf";
        string outputPath = "published.pdf";

        // Define the XMP fields that must be present.
        // Adjust the list according to your publication requirements.
        string[] requiredFields = new string[]
        {
            "dc:title",
            "dc:creator",
            "dc:description",
            "pdf:Producer"
        };

        try
        {
            ValidateAndPublish(inputPath, requiredFields, outputPath);
            Console.WriteLine($"PDF validated and published to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation failed: {ex.Message}");
        }
    }
}