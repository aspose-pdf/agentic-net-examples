using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string encryptedPdf = "encrypted.pdf";
        const string signedPdf = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "certPassword";

        if (!File.Exists(inputPdf) || !File.Exists(certPath))
        {
            Console.Error.WriteLine("Input PDF or certificate file not found.");
            return;
        }

        // Load the PDF and encrypt it using a certificate (AES‑256)
        using (Document doc = new Document(inputPdf))
        {
            X509Certificate2 encryptionCert = new X509Certificate2(certPath, certPassword);
            var certs = new List<X509Certificate2> { encryptionCert };

            // Encrypt for recipients that possess the private key of the certificate
            doc.Encrypt(Permissions.PrintDocument, CryptoAlgorithm.AESx256, certs);
            doc.Save(encryptedPdf);
        }

        // Sign the encrypted PDF; the signature will be required to open the document
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(encryptedPdf);

        // Optional: set a visual appearance for the signature
        // pdfSigner.SignatureAppearance = "signatureImage.png";

        // Provide the signing certificate (can be the same as the encryption certificate)
        pdfSigner.SetCertificate(certPath, certPassword);

        // Define a visible signature rectangle (System.Drawing.Rectangle is required by the API)
        System.Drawing.Rectangle sigRect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Sign page 1 with visible signature
        pdfSigner.Sign(
            page: 1,
            SigReason: "Document requires a digital signature to open",
            SigContact: "contact@example.com",
            SigLocation: "Office",
            visible: true,
            annotRect: sigRect);

        // Save the signed PDF
        pdfSigner.Save(signedPdf);
        pdfSigner.Close();

        Console.WriteLine($"Signed and encrypted PDF saved to '{signedPdf}'.");
    }
}