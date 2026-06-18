using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string auditXml = "signature_audit.xml";
        const string fullStructureXml = "full_structure.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Create an XML file that contains only the signature information
            using (XmlWriter writer = XmlWriter.Create(auditXml, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Signatures");

                // Iterate over all fields and filter for signature fields
                foreach (Field field in doc.Form.Fields)
                {
                    if (field is SignatureField sigField)
                    {
                        writer.WriteStartElement("Signature");
                        writer.WriteAttributeString("Name", sigField.FullName ?? string.Empty);

                        // The Signature object holds the detailed signature data
                        var signature = sigField.Signature;
                        if (signature != null)
                        {
                            writer.WriteElementString("Authority", signature.Authority ?? string.Empty);
                            writer.WriteElementString("Location", signature.Location ?? string.Empty);
                            writer.WriteElementString("Reason", signature.Reason ?? string.Empty);
                            writer.WriteElementString("ContactInfo", signature.ContactInfo ?? string.Empty);

                            // The SignDate property is not available in all versions; use a safe fallback
                            string dateValue = string.Empty;
                            try
                            {
                                // If the property exists, it will be accessed; otherwise an exception is caught
                                var signDateProp = signature.GetType().GetProperty("SignDate");
                                if (signDateProp != null)
                                {
                                    var value = signDateProp.GetValue(signature) as DateTime?;
                                    if (value.HasValue)
                                        dateValue = value.Value.ToString("o");
                                }
                            }
                            catch { /* ignore any reflection errors */ }
                            writer.WriteElementString("Date", dateValue);

                            var algoInfo = signature.GetSignatureAlgorithmInfo();
                            writer.WriteElementString("AlgorithmInfo", algoInfo?.ToString() ?? string.Empty);
                        }
                        else
                        {
                            // No signature applied – write empty placeholders
                            writer.WriteElementString("Authority", string.Empty);
                            writer.WriteElementString("Location", string.Empty);
                            writer.WriteElementString("Reason", string.Empty);
                            writer.WriteElementString("ContactInfo", string.Empty);
                            writer.WriteElementString("Date", string.Empty);
                            writer.WriteElementString("AlgorithmInfo", string.Empty);
                        }

                        writer.WriteEndElement(); // </Signature>
                    }
                }

                writer.WriteEndElement(); // </Signatures>
                writer.WriteEndDocument();
            }

            // Optionally, save the entire PDF structure as XML using XmlSaveOptions
            // (non‑PDF save must use explicit SaveOptions per rule)
            doc.Save(fullStructureXml, new XmlSaveOptions());
        }

        Console.WriteLine($"Signature audit XML saved to '{auditXml}'.");
        Console.WriteLine($"Full PDF structure XML saved to '{fullStructureXml}'.");
    }
}
