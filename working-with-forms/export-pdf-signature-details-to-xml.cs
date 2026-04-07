using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputXml = "signature_audit.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Prepare the root XML element
            XElement signaturesRoot = new XElement("Signatures");

            // Iterate over all fields and pick the signature fields
            if (pdfDoc?.Form?.Fields != null)
            {
                foreach (var field in pdfDoc.Form.Fields)
                {
                    if (field is SignatureField sigField)
                    {
                        // The Signature object associated with the field (may be null if not signed yet)
                        Signature signature = sigField.Signature;

                        // Basic signature properties – use empty strings when the signature object is null
                        string fieldName   = sigField.PartialName ?? string.Empty;
                        string date        = string.Empty;
                        string reason      = string.Empty;
                        string location    = string.Empty;
                        string authority   = string.Empty;
                        string contactInfo = string.Empty;

                        if (signature != null)
                        {
                            date        = signature.Date.ToString("o");
                            reason      = signature.Reason ?? string.Empty;
                            location    = signature.Location ?? string.Empty;
                            authority   = signature.Authority ?? string.Empty;
                            contactInfo = signature.ContactInfo ?? string.Empty;
                        }

                        // Algorithm information (may be null if not signed yet)
                        string algorithmName = string.Empty;
                        string cryptoStandard = string.Empty;
                        string digestAlgorithm = string.Empty;
                        string algorithmType = string.Empty;

                        if (signature != null)
                        {
                            var algoInfo = signature.GetSignatureAlgorithmInfo();
                            if (algoInfo != null)
                            {
                                algorithmName = algoInfo.SignatureName ?? string.Empty;
                                if (algoInfo is KeyedSignatureAlgorithmInfo keyedInfo)
                                {
                                    // Enums are value types; they cannot be null‑conditional. Use direct ToString().
                                    algorithmType   = keyedInfo.AlgorithmType.ToString();
                                    cryptoStandard  = keyedInfo.CryptographicStandard.ToString();
                                    digestAlgorithm = keyedInfo.DigestHashAlgorithm.ToString();
                                }
                            }
                        }

                        // Extract the certificate (if present) and encode it as Base64
                        string certificateBase64 = string.Empty;
                        using (Stream certStream = sigField.ExtractCertificate())
                        {
                            if (certStream != null && certStream.Length > 0)
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    certStream.CopyTo(ms);
                                    certificateBase64 = Convert.ToBase64String(ms.ToArray());
                                }
                            }
                        }

                        // Build XML element for this signature
                        XElement sigElement = new XElement("Signature",
                            new XAttribute("FieldName", fieldName),
                            new XElement("Date", date),
                            new XElement("Reason", reason),
                            new XElement("Location", location),
                            new XElement("Authority", authority),
                            new XElement("ContactInfo", contactInfo),
                            new XElement("AlgorithmName", algorithmName),
                            new XElement("AlgorithmType", algorithmType),
                            new XElement("CryptographicStandard", cryptoStandard),
                            new XElement("DigestHashAlgorithm", digestAlgorithm),
                            new XElement("CertificateBase64", certificateBase64)
                        );

                        signaturesRoot.Add(sigElement);
                    }
                }
            }

            // Save the constructed XML document
            XDocument auditDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), signaturesRoot);
            auditDoc.Save(outputXml);
        }

        Console.WriteLine($"Signature audit XML saved to '{outputXml}'.");
    }
}
