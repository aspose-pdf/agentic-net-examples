using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Root element for the audit XML
            XElement root = new XElement("Signatures");

            // Iterate over all fields and filter for signature fields
            foreach (Field field in pdfDoc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Create an element for each signature
                    XElement sigElem = new XElement("Signature",
                        new XAttribute("Name", sigField.PartialName ?? string.Empty));

                    // Basic signature properties (may be null)
                    if (sigField.Signature != null)
                    {
                        sigElem.Add(new XElement("Date", sigField.Signature.Date.ToString("o")));
                        sigElem.Add(new XElement("Reason", sigField.Signature.Reason ?? string.Empty));
                        sigElem.Add(new XElement("Location", sigField.Signature.Location ?? string.Empty));
                        sigElem.Add(new XElement("ContactInfo", sigField.Signature.ContactInfo ?? string.Empty));
                        sigElem.Add(new XElement("Authority", sigField.Signature.Authority ?? string.Empty));

                        // Algorithm information, if available
                        var algoInfo = sigField.Signature.GetSignatureAlgorithmInfo();
                        if (algoInfo != null)
                        {
                            sigElem.Add(new XElement("AlgorithmInfo", algoInfo.ToString()));
                        }
                    }
                    else
                    {
                        // No signature object – add empty placeholders
                        sigElem.Add(new XElement("Date", string.Empty));
                        sigElem.Add(new XElement("Reason", string.Empty));
                        sigElem.Add(new XElement("Location", string.Empty));
                        sigElem.Add(new XElement("ContactInfo", string.Empty));
                        sigElem.Add(new XElement("Authority", string.Empty));
                    }

                    // Extract the embedded certificate (if any) and store it as Base64
                    try
                    {
                        using (Stream certStream = sigField.ExtractCertificate())
                        {
                            if (certStream != null && certStream.Length > 0)
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    certStream.CopyTo(ms);
                                    string base64 = Convert.ToBase64String(ms.ToArray());
                                    sigElem.Add(new XElement("CertificateBase64", base64));
                                }
                            }
                        }
                    }
                    catch
                    {
                        // No certificate present – ignore
                    }

                    root.Add(sigElem);
                }
            }

            // Save the constructed XML document to file
            XDocument auditDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            auditDoc.Save(outputXml);
        }

        Console.WriteLine($"Signature audit XML saved to '{outputXml}'.");
    }
}
