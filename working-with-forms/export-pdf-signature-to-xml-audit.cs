using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string outputXml = "signature_audit.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has an AcroForm
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Create the root element for the audit XML
            XElement root = new XElement("Signatures");

            // Iterate over each field and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // The actual signature object may be null if the field is not signed yet
                    Signature signature = sigField.Signature;
                    if (signature == null)
                        continue;

                    // Build an XML element with the most relevant signature properties
                    XElement sigElem = new XElement("Signature",
                        new XAttribute("FieldName", sigField.PartialName ?? string.Empty),
                        new XElement("Authority", signature.Authority ?? string.Empty),
                        new XElement("Date", signature.Date.ToString("o")),
                        new XElement("Reason", signature.Reason ?? string.Empty),
                        new XElement("Location", signature.Location ?? string.Empty),
                        new XElement("ContactInfo", signature.ContactInfo ?? string.Empty)
                    );

                    // If additional algorithm information is needed, it can be retrieved via the
                    // Signature object's properties (if available). The previous example used a
                    // non‑existent GetSignatureAlgorithmInfo() method, so it has been omitted.

                    root.Add(sigElem);
                }
            }

            // Save the constructed XML to the specified file
            XDocument auditDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            auditDoc.Save(outputXml);
            Console.WriteLine($"Signature audit information saved to '{outputXml}'.");
        }
    }
}
