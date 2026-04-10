using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF containing a signature field
        const string xmlSigPath = "signatureInfo.xml"; // XML with signing data
        const string outputPdf = "signed_output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlSigPath))
        {
            Console.Error.WriteLine($"XML signature info not found: {xmlSigPath}");
            return;
        }

        // Load the XML that contains the path to the PFX file and its password.
        // Expected format:
        // <SignatureInfo>
        //   <PfxPath>mycert.pfx</PfxPath>
        //   <Password>pfxPassword</Password>
        //   <Reason>Document approval</Reason>
        //   <Location>New York, USA</Location>
        //   <ContactInfo>john.doe@example.com</ContactInfo>
        //   <SignatureFieldName>Signature1</SignatureFieldName>
        // </SignatureInfo>
        XDocument xmlDoc = XDocument.Load(xmlSigPath);
        string pfxPath = xmlDoc.Root?.Element("PfxPath")?.Value ?? string.Empty;
        string password = xmlDoc.Root?.Element("Password")?.Value ?? string.Empty;
        string reason = xmlDoc.Root?.Element("Reason")?.Value ?? string.Empty;
        string location = xmlDoc.Root?.Element("Location")?.Value ?? string.Empty;
        string contact = xmlDoc.Root?.Element("ContactInfo")?.Value ?? string.Empty;
        string fieldName = xmlDoc.Root?.Element("SignatureFieldName")?.Value ?? string.Empty;

        if (string.IsNullOrWhiteSpace(pfxPath) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Invalid or missing PFX file path in XML.");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Locate the signature field by its full name.
            // If the field name is not provided, take the first signature field found.
            SignatureField sigField = null;
            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                foreach (Field f in pdfDoc.Form.Fields)
                {
                    if (f is SignatureField sf && sf.FullName.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                    {
                        sigField = sf;
                        break;
                    }
                }
            }

            // Fallback: take the first signature field if specific name not found.
            if (sigField == null)
            {
                foreach (Field f in pdfDoc.Form.Fields)
                {
                    if (f is SignatureField sf)
                    {
                        sigField = sf;
                        break;
                    }
                }
            }

            if (sigField == null)
            {
                Console.Error.WriteLine("No signature field found in the PDF.");
                return;
            }

            // Create a concrete PKCS7 signature object using the PFX file and password.
            // PKCS7 derives from the abstract Signature class and can be used with SignatureField.Sign().
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, password);

            // Populate optional signature properties from XML.
            pkcs7Signature.Reason = reason;
            pkcs7Signature.Location = location;
            pkcs7Signature.ContactInfo = contact;
            pkcs7Signature.Date = DateTime.Now;

            // Apply the signature to the field.
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
