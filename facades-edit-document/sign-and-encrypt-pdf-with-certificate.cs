using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Drawing; // needed for System.Drawing.Rectangle used by PdfFileSignature
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for PKCS1

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string signedPdf = "signed.pdf";
        const string encryptedPdf = "encrypted.pdf";

        const string certPfxPath = "certificate.pfx";   // signing certificate (contains private key)
        const string certPfxPassword = "password";

        const string encryptionCertPath = "encryption_cert.cer"; // public certificate for encryption
        // If the encryption certificate is also a .pfx, you can load it similarly:
        // const string encryptionCertPath = "encryption_cert.pfx";
        // const string encryptionCertPassword = "encPassword";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPfxPath))
        {
            Console.Error.WriteLine($"Signing certificate not found: {certPfxPath}");
            return;
        }
        if (!File.Exists(encryptionCertPath))
        {
            Console.Error.WriteLine($"Encryption certificate not found: {encryptionCertPath}");
            return;
        }

        // ---------- Step 1: Sign the PDF ----------
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the signature facade and bind the document
            PdfFileSignature pdfSign = new PdfFileSignature();
            pdfSign.BindPdf(doc);

            // Create PKCS#1 object from the signing certificate (private key required)
            PKCS1 pkcs1 = new PKCS1(certPfxPath, certPfxPassword);

            // Define a visible rectangle for the signature on page 1 (System.Drawing.Rectangle)
            System.Drawing.Rectangle sigRect = new System.Drawing.Rectangle(100, 100, 200, 50);

            // Sign the document using the overload that accepts PKCS1 and a System.Drawing.Rectangle
            // Use positional arguments to match the exact overload signature.
            pdfSign.Sign(
                1,                     // page number (1‑based)
                "Document signed",   // reason
                "contact@example.com", // contact info
                "Location",          // location
                true,                  // isVisible
                sigRect,               // rectangle
                pkcs1);                // PKCS#1 object

            // Save the signed PDF to a temporary file
            pdfSign.Save(signedPdf);
        }

        // ---------- Step 2: Encrypt the signed PDF with certificate‑based encryption ----------
        // Load the signed PDF
        using (Document signedDoc = new Document(signedPdf))
        {
            // Load the public certificate used for encryption using the modern loader.
            // X509CertificateLoader.LoadCertificate expects a byte[] containing the certificate data.
            X509Certificate2 encryptionCert = X509CertificateLoader.LoadCertificate(File.ReadAllBytes(encryptionCertPath));
            IList<X509Certificate2> certList = new List<X509Certificate2> { encryptionCert };

            // Define desired permissions (example: allow printing, disallow modifications)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using AES‑256 and the public certificate(s)
            signedDoc.Encrypt(perms, CryptoAlgorithm.AESx256, certList);

            // Save the encrypted PDF
            signedDoc.Save(encryptedPdf);
        }

        Console.WriteLine($"Signed PDF saved to: {signedPdf}");
        Console.WriteLine($"Encrypted PDF saved to: {encryptedPdf}");
    }
}
