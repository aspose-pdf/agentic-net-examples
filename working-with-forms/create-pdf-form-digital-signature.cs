using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPdfPath = "form_with_signature.pdf";
        const string certPath = "signer_certificate.pfx";   // path to the signing certificate
        const string certPassword = "certPassword";         // password for the .pfx file

        // Verify that the certificate file exists before proceeding
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Create a new PDF document and wrap it in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form fields
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Create a digital signature field
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            SignatureField signatureField = new SignatureField(doc, sigRect)
            {
                PartialName = "DigitalSignature1"   // internal name of the field
            };

            // Add the signature field to the form (page 1)
            doc.Form.Add(signatureField, 1);

            // ------------------------------------------------------------
            // 2. Configure the PKCS7 digital signature
            // ------------------------------------------------------------
            // Load the signing certificate via PKCS7 helper class
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document approval",
                Location = "Office",
                ContactInfo = "signer@example.com"
                // Note: Aspose.Pdf does not expose a ValidateCertificate property.
                // Validation is performed by PDF viewers based on the embedded certificate chain.
            };

            // Sign the field – the Signature property is read‑only, so we use the Sign method
            signatureField.Sign(pkcs7);

            // ------------------------------------------------------------
            // 3. Save the PDF form
            // ------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF form with digital signature field saved to '{outputPdfPath}'.");
    }
}
