using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string signatureXmlPath = "signature.xml";
        const string outputPdfPath = "signed_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(signatureXmlPath))
        {
            Console.Error.WriteLine($"Signature XML not found: {signatureXmlPath}");
            return;
        }

        try
        {
            // -------------------------------------------------------------------
            // Load signature parameters from the XML file.
            // Expected XML format (all elements are optional except CertificatePath):
            //   <Signature>
            //       <FieldName>Signature1</FieldName>
            //       <CertificatePath>cert.pfx</CertificatePath>
            //       <Password>pwd</Password>
            //       <Reason>Approved</Reason>
            //       <Location>Office</Location>
            //   </Signature>
            // -------------------------------------------------------------------
            XDocument xml = XDocument.Load(signatureXmlPath);
            XElement root = xml.Root ?? new XElement("Signature");
            string fieldName = (string)root.Element("FieldName") ?? string.Empty;
            string certPath = (string)root.Element("CertificatePath") ?? string.Empty;
            string certPassword = (string)root.Element("Password") ?? string.Empty;
            string reason = (string)root.Element("Reason") ?? string.Empty;
            string location = (string)root.Element("Location") ?? string.Empty;

            if (string.IsNullOrWhiteSpace(certPath) || !File.Exists(certPath))
            {
                Console.Error.WriteLine($"Certificate file not found: {certPath}");
                return;
            }

            // -------------------------------------------------------------------
            // Load the PDF document.
            // -------------------------------------------------------------------
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Find the signature field. If a field name is supplied we look for that name;
                // otherwise we take the first SignatureField we encounter.
                SignatureField signatureField = null;
                foreach (Field f in pdfDoc.Form.Fields)
                {
                    if (f is SignatureField sf)
                    {
                        if (string.IsNullOrEmpty(fieldName) ||
                            sf.PartialName.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                        {
                            signatureField = sf;
                            break;
                        }
                    }
                }

                if (signatureField == null)
                {
                    Console.Error.WriteLine(string.IsNullOrEmpty(fieldName)
                        ? "No signature field found in the PDF."
                        : $"Signature field '{fieldName}' not found in the PDF.");
                    return;
                }

                // -------------------------------------------------------------------
                // Create a PKCS7 signature object using the certificate.
                // -------------------------------------------------------------------
                PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
                if (!string.IsNullOrEmpty(reason)) pkcs7.Reason = reason;
                if (!string.IsNullOrEmpty(location)) pkcs7.Location = location;

                // -------------------------------------------------------------------
                // Apply the signature to the field. The Signature property is read‑only;
                // the correct way is to call the Sign method on the field.
                // -------------------------------------------------------------------
                signatureField.Sign(pkcs7);

                // Save the signed PDF.
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
