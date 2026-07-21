using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF containing a signature field
        const string xmlPath = "signatureData.xml"; // XML with digital signature data (XFA)
        const string pfxPath = "certificate.pfx";   // Certificate used for signing
        const string pfxPassword = "pfxPassword";   // Password for the PFX file
        const string outputPdf = "signed_output.pdf";

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
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle: create → load → save)
        using (Document doc = new Document(pdfPath))
        {
            // Load the XML containing the signature data
            XmlDocument xfaXml = new XmlDocument();
            xfaXml.Load(xmlPath);

            // Assign the XFA data to the PDF form.
            // This imports the digital signature information stored in the XML.
            doc.Form.AssignXfa(xfaXml);

            // Locate the signature field by its full name.
            const string signatureFieldName = "Signature1";

            // Retrieve the field; if it does not exist or is not a SignatureField, handle the error.
            var sigField = doc.Form[signatureFieldName] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found or is not a signature field.");
                return;
            }

            // Use a concrete PKCS7 signature object (Signature is abstract).
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document signed",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Apply the signature to the field.
            sigField.Sign(pkcs7);

            // Save the signed PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
