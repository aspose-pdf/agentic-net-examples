using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportSignatureInfo
{
    static void Main()
    {
        const string pdfPath = "signed.pdf";
        const string xmlPath = "signature_audit.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Root element for the XML audit file
            XElement signaturesRoot = new XElement("Signatures");

            // Iterate over all form fields and pick the signature fields
            if (pdfDoc?.Form?.Fields != null)
            {
                foreach (Field field in pdfDoc.Form.Fields)
                {
                    if (field is SignatureField sigField)
                    {
                        // Basic field information
                        XElement sigElement = new XElement("Signature",
                            new XAttribute("FieldName", sigField.PartialName ?? string.Empty),
                            new XElement("Rect",
                                sigField.Rect != null
                                    ? $"{sigField.Rect.LLX},{sigField.Rect.LLY},{sigField.Rect.URX},{sigField.Rect.URY}"
                                    : string.Empty)
                        );

                        // Information stored on the PKCS7 signature (if the field is signed)
                        if (sigField.Signature != null)
                        {
                            // The concrete signature object is usually PKCS7
                            if (sigField.Signature is PKCS7 pkcs)
                            {
                                sigElement.Add(
                                    new XElement("Reason",       pkcs.Reason       ?? string.Empty),
                                    new XElement("Location",     pkcs.Location     ?? string.Empty),
                                    new XElement("ContactInfo",  pkcs.ContactInfo  ?? string.Empty)
                                    // NOTE: PKCS7 does not expose SigningTime, SignatureAlgorithm or DigestAlgorithm directly.
                                );
                            }

                            // Extract the signing certificate (if present)
                            try
                            {
                                X509Certificate2 cert = sigField.ExtractCertificateObject();
                                if (cert != null)
                                {
                                    sigElement.Add(
                                        new XElement("CertificateSubject", cert.Subject ?? string.Empty),
                                        new XElement("CertificateIssuer",  cert.Issuer  ?? string.Empty),
                                        new XElement("ValidFrom",          cert.NotBefore.ToString("o")),
                                        new XElement("ValidTo",            cert.NotAfter.ToString("o"))
                                    );
                                }
                            }
                            catch
                            {
                                // If the certificate cannot be extracted, ignore – the audit will still contain other data.
                            }
                        }

                        signaturesRoot.Add(sigElement);
                    }
                }
            }

            // Build the final XML document
            XDocument auditXml = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                signaturesRoot
            );

            // Save the XML audit file
            auditXml.Save(xmlPath);
            Console.WriteLine($"Signature audit saved to '{xmlPath}'.");
        }
    }
}
