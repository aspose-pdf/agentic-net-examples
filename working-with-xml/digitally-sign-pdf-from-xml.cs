using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath = "input.xml";
        const string outputPdf = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        // Validate input files
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Create PDF from XML
        using (Document doc = new Document())
        {
            // Bind the XML content to the document (no XSLT used)
            doc.BindXml(xmlPath);

            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Define the rectangle where the visible signature will appear
            // (llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName = "Signature1" // optional field name
            };

            // Add the signature field to the document's form on page 1
            doc.Form.Add(sigField, 1);

            // Prepare a PKCS#1 signature object using the certificate
            PKCS1 pkcs1 = new PKCS1(certPath, certPassword)
            {
                Reason = "Document approved",
                ContactInfo = "contact@example.com",
                Location = "New York"
            };

            // Sign the field – this embeds the digital signature into the PDF
            sigField.Sign(pkcs1);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}