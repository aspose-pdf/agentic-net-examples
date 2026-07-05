using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF with a signature field
        const string xmlPath = "signatureData.xml";  // XML containing signature data (XFA)
        const string pfxPath = "certificate.pfx";    // PFX file with signing certificate
        const string pfxPassword = "password";       // Password for the PFX file
        const string outputPath = "signed_output.pdf"; // Resulting signed PDF

        // Verify required files exist
        if (!File.Exists(pdfPath) || !File.Exists(xmlPath) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Load the XML that holds the digital signature data (XFA)
            XmlDocument xfaXml = new XmlDocument();
            xfaXml.Load(xmlPath);

            // Assign the XFA data to the PDF form
            doc.Form.AssignXfa(xfaXml);

            // Locate the signature field by its full name and cast to the proper type
            Field? field = doc.Form["Signature1"] as Field;
            if (field is SignatureField signatureField)
            {
                // Use a concrete implementation of the abstract Signature class
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
                {
                    Reason = "Document approved",
                    Location = "Office",
                    ContactInfo = "contact@example.com"
                };

                // Apply the signature to the field
                signatureField.Sign(pkcs7);
            }
            else
            {
                Console.Error.WriteLine("Signature field 'Signature1' not found in the document.");
            }

            // Save the signed PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
