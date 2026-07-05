using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF to be secured
        const string inputPdf = "input.pdf";

        // Paths for intermediate signed PDF and final encrypted PDF
        const string signedPdf = "signed.pdf";
        const string outputPdf = "secured.pdf";

        // Signing certificate (PKCS#12) and its password
        const string signingPfx = "signing_certificate.pfx";
        const string signingPassword = "signPassword";

        // Public certificate of the recipient used for certificate‑based encryption
        const string recipientCert = "recipient_certificate.cer";

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(signingPfx))
        {
            Console.Error.WriteLine($"Signing certificate not found: {signingPfx}");
            return;
        }
        if (!File.Exists(recipientCert))
        {
            Console.Error.WriteLine($"Recipient certificate not found: {recipientCert}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Digitally sign (certify) the PDF using PdfFileSignature
        // ------------------------------------------------------------
        PdfFileSignature signer = new PdfFileSignature();
        signer.BindPdf(inputPdf);                                   // Load the PDF
        signer.SetCertificate(signingPfx, signingPassword);        // Private key for signing
        signer.SignatureAppearance = "signature_appearance.png";    // Optional visual appearance

        // Define visible signature rectangle (System.Drawing.Rectangle)
        System.Drawing.Rectangle sigRect = new System.Drawing.Rectangle(100, 100, 200, 50);

        // Create a visible signature on page 1
        signer.Sign(
            page: 1,
            SigReason: "Document certification",
            SigContact: "john.doe@example.com",
            SigLocation: "New York",
            visible: true,
            annotRect: sigRect);

        // Save the signed PDF to an intermediate file
        signer.Save(signedPdf);
        signer.Close(); // Facade does not implement IDisposable; explicit close is optional

        // ------------------------------------------------------------
        // Step 2: Encrypt the signed PDF with certificate‑based encryption
        // ------------------------------------------------------------
        // Load the signed PDF into a Document object (using ensures proper disposal)
        using (Document doc = new Document(signedPdf))
        {
            // Load the recipient's public certificate
            List<X509Certificate2> recipientCertificates = new List<X509Certificate2>
            {
                new X509Certificate2(recipientCert)
            };

            // Encrypt the document for the recipient.
            // Permissions can be adjusted as needed; here we allow printing only.
            doc.Encrypt(
                permissions: Permissions.PrintDocument,
                cryptoAlgorithm: CryptoAlgorithm.AESx256,
                publicCertificates: recipientCertificates);

            // Save the encrypted PDF; the file can now be opened only by holders of the private key
            // corresponding to the public certificate used above.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF has been signed and encrypted successfully: {outputPdf}");
    }
}