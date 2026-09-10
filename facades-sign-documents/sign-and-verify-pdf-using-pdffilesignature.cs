using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf = "input.pdf";
        const string signedPdf = "signed.pdf";
        const string certFile = "certificate.pfx";
        const string certPassword = "password";

        // ------------------------------------------------------------
        // 1. Ensure a source PDF exists (create a minimal placeholder)
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdf);
        }

        // ------------------------------------------------------------
        // 2. Ensure a signing certificate exists (create a self‑signed PFX)
        // ------------------------------------------------------------
        if (!File.Exists(certFile))
        {
            using RSA rsa = RSA.Create(2048);
            var req = new CertificateRequest(
                "cn=AsposeTest",
                rsa,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            // Basic constraints – self‑signed, not a CA
            req.CertificateExtensions.Add(
                new X509BasicConstraintsExtension(false, false, 0, false));
            // Key usage – digital signature
            req.CertificateExtensions.Add(
                new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, false));

            var cert = req.CreateSelfSigned(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddYears(1));
            byte[] pfxBytes = cert.Export(X509ContentType.Pfx, certPassword);
            File.WriteAllBytes(certFile, pfxBytes);
        }

        // ------------------------------------------------------------
        // 3. Sign the PDF and immediately verify the signature
        // ------------------------------------------------------------
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            // Load the source PDF
            signer.BindPdf(inputPdf);

            // Set the certificate used for signing (load from the generated PFX)
            signer.SetCertificate(certFile, certPassword);

            // Define the visible signature rectangle (System.Drawing.Rectangle)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Create a visible signature on page 1
            signer.Sign(
                page: 1,
                SigReason: "Document approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: rect);

            // Save the signed PDF
            signer.Save(signedPdf);

            // Verify all signatures present in the document
            IList<SignatureName> signatureNames = signer.GetSignatureNames(true);
            foreach (SignatureName sigName in signatureNames)
            {
                bool isValid = signer.VerifySignature(sigName);
                Console.WriteLine($"Signature '{sigName.Name}' validity: {isValid}");
            }
        }
    }
}
