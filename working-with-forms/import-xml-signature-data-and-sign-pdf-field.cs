using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // needed for PKCS7

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF containing a signature field
        const string xmlPath = "signatureData.xml";  // XML with XFA data (digital signature info)
        const string pfxPath = "certificate.pfx";   // Certificate used to sign
        const string pfxPassword = "pfxPassword";   // Password for the PFX file
        const string outputPdf = "signed_output.pdf";

        if (!File.Exists(pdfPath) || !File.Exists(xmlPath) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Load the XML containing XFA data and assign it to the form
            XmlDocument xfaXml = new XmlDocument();
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            {
                xfaXml.Load(xmlStream);
            }
            // Assign the XFA data to the PDF form (if the PDF contains an XFA form)
            doc.Form.AssignXfa(xfaXml);

            // Locate the signature field by its name (adjust the name as needed)
            const string signatureFieldName = "Signature1";
            SignatureField sigField = doc.Form[signatureFieldName] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
                return;
            }

            // Create a PKCS7 signature object from the PFX certificate (Signature is abstract)
            PKCS7 pkcs7;
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                pkcs7 = new PKCS7(pfxStream, pfxPassword);
            }

            // Optional: set additional signature properties
            pkcs7.Reason = "Document approved";
            pkcs7.Location = "Office";
            pkcs7.ContactInfo = "contact@example.com";

            // Apply the signature to the field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
