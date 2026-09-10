using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // for PKCS7 and SignatureField classes

class ApplySignatureFromXml
{
    static void Main()
    {
        // Input PDF, XML (XFDF‑like) containing signature data, and certificate (PFX)
        const string pdfPath = "input.pdf";
        const string xmlPath = "signature_data.xml"; // renamed for clarity
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";
        const string outputPath = "signed_output.pdf";

        // Verify required files exist
        if (!File.Exists(pdfPath) || !File.Exists(xmlPath) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Missing input PDF, XML, or PFX file.");
            return;
        }

        // Load the PDF document (lifecycle: create/load)
        using (Document doc = new Document(pdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Parse the XML that contains the signature field data.
            //    The original example used Aspose.Pdf.Xfdf which lives in a separate
            //    assembly. To keep the project self‑contained we read the XML with
            //    System.Xml and extract the values we need (field name, reason,
            //    location, contact info, etc.).
            // -----------------------------------------------------------------
            string targetFieldName = "Signature1"; // default field name
            string reason = "Document approved";
            string location = "Office";
            string contactInfo = "contact@example.com";

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                // Expected simple structure:
                // <xfdf>
                //   <fields>
                //     <field name="Signature1">
                //       <reason>...</reason>
                //       <location>...</location>
                //       <contactInfo>...</contactInfo>
                //     </field>
                //   </fields>
                // </xfdf>
                XmlNode fieldNode = xmlDoc.SelectSingleNode($"//field[@name='{targetFieldName}']");
                if (fieldNode != null)
                {
                    XmlNode reasonNode = fieldNode.SelectSingleNode("reason");
                    XmlNode locationNode = fieldNode.SelectSingleNode("location");
                    XmlNode contactNode = fieldNode.SelectSingleNode("contactInfo");

                    if (reasonNode != null) reason = reasonNode.InnerText;
                    if (locationNode != null) location = locationNode.InnerText;
                    if (contactNode != null) contactInfo = contactNode.InnerText;
                }
                else
                {
                    Console.Error.WriteLine($"Signature field '{targetFieldName}' not found in XML. Using defaults.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading XML signature data: {ex.Message}. Using defaults.");
            }

            // -----------------------------------------------------------------
            // 2. Locate the signature field inside the PDF.
            // -----------------------------------------------------------------
            SignatureField sigField = doc.Form[targetFieldName] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine($"Signature field '{targetFieldName}' not found in PDF.");
                return;
            }

            // -----------------------------------------------------------------
            // 3. Create a PKCS7 signature object using the certificate.
            // -----------------------------------------------------------------
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason = reason,
                    Location = location,
                    ContactInfo = contactInfo
                };

                // Apply the digital signature to the field.
                sigField.Sign(pkcs7);
            }

            // Save the signed PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
