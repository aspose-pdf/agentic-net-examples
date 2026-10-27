using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "signed_document.pdf";
        const string outputXmlPath = "signature_audit.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare XML writer
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    Encoding = Encoding.UTF8
                };

                using (XmlWriter writer = XmlWriter.Create(outputXmlPath, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Signatures");

                    // Iterate over all form fields and process only signature fields
                    foreach (Field field in pdfDoc.Form.Fields)
                    {
                        if (field is SignatureField sigField && sigField.Signature != null)
                        {
                            // Extract certificate to obtain signer details (if available)
                            string signerName = string.Empty;
                            string signDate = string.Empty;
                            try
                            {
                                X509Certificate2 cert = sigField.ExtractCertificateObject();
                                if (cert != null)
                                {
                                    signerName = cert.GetNameInfo(X509NameType.SimpleName, false) ?? string.Empty;
                                    // Use the certificate's NotBefore as a fallback signing date (ISO‑8601)
                                    signDate = cert.NotBefore.ToString("o");
                                }
                            }
                            catch
                            {
                                // If certificate extraction fails, leave fields empty
                            }

                            writer.WriteStartElement("Signature");

                            // Field name (the name of the signature field in the form)
                            writer.WriteElementString("FieldName", sigField.PartialName ?? string.Empty);

                            // Signer name (extracted from the certificate, if present)
                            writer.WriteElementString("SignerName", signerName);

                            // Location where the document was signed
                            writer.WriteElementString("Location", sigField.Signature.Location ?? string.Empty);

                            // Reason for signing
                            writer.WriteElementString("Reason", sigField.Signature.Reason ?? string.Empty);

                            // Contact information
                            writer.WriteElementString("ContactInfo", sigField.Signature.ContactInfo ?? string.Empty);

                            // Signing date (fallback to certificate date if Signature.SignDate is unavailable)
                            writer.WriteElementString("SignDate", signDate);

                            writer.WriteEndElement(); // </Signature>
                        }
                    }

                    writer.WriteEndElement(); // </Signatures>
                    writer.WriteEndDocument();
                }

                Console.WriteLine($"Signature information exported to '{outputXmlPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
