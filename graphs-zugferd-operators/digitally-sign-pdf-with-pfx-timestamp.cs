using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";
        const string timestampServer = "http://timestamp.digicert.com";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX certificate not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdfPath);

        // Create a visible signature field on the first page
        // Rectangle uses 1‑based coordinates: llx, lly, urx, ury
        Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
        SignatureField sigField = new SignatureField(doc, sigRect)
        {
            PartialName = "Signature1"
        };
        // Add the field to the form on page 1
        doc.Form.Add(sigField, 1);

        // Prepare the PKCS7 signature object using the PFX certificate
        PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
        pkcs7.Reason = "Document approval";
        pkcs7.Location = "New York, USA";
        pkcs7.ContactInfo = "support@example.com";

        // Configure timestamp server (basicAuthCredentials left empty)
        pkcs7.TimestampSettings = new Aspose.Pdf.TimestampSettings(
            timestampServer,
            string.Empty,
            Aspose.Pdf.DigestHashAlgorithm.Sha256);

        // Sign the field
        sigField.Sign(pkcs7);

        // Save the signed PDF
        doc.Save(outputPdfPath);

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}
