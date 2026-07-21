using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";

        // Custom XFA data to embed (replace with actual XFA XML)
        const string xfaXml = @"<?xml version='1.0' encoding='UTF-8'?>
<xfa:datasets xmlns:xfa='http://www.xfa.org/schema/xfa-data/1.0/'>
  <xfa:data>
    <myField>Custom Value</myField>
  </xfa:data>
</xfa:datasets>";

        if (!File.Exists(inputPdfPath) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Required file not found.");
            return;
        }

        // Load the PDF (using the standard Document constructor)
        using (Document doc = new Document(inputPdfPath))
        {
            // -------------------------------------------------
            // 1. Embed custom XFA data into the form (if any)
            // -------------------------------------------------
            XmlDocument xfaDoc = new XmlDocument();
            xfaDoc.LoadXml(xfaXml);
            // AssignXfa sets the XFA stream of the document
            doc.Form.AssignXfa(xfaDoc);

            // -------------------------------------------------
            // 2. Create a signature field on the first page
            // -------------------------------------------------
            // Fully qualified Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc, sigRect);
            // Add the signature field to the page's annotation collection
            doc.Pages[1].Annotations.Add(sigField);

            // -------------------------------------------------
            // 3. Prepare the signature object (PKCS#7)
            // -------------------------------------------------
            // Use the concrete PKCS7 class (Signature is abstract)
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason       = "Document approval",
                Location     = "Office",
                ContactInfo  = "contact@example.com",
                Date         = DateTime.UtcNow
            };

            // -------------------------------------------------
            // 4. Sign the document using the signature field
            // -------------------------------------------------
            sigField.Sign(pkcs7);

            // -------------------------------------------------
            // 5. Save the signed PDF
            // -------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
