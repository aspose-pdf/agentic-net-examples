using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "ecc_cert.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Load the ECC certificate (must contain a P‑256 private key)
        X509Certificate2 cert = new X509Certificate2(
            pfxPath,
            pfxPassword,
            X509KeyStorageFlags.Exportable);

        using ECDsa ecdsa = cert.GetECDsaPrivateKey();
        if (ecdsa == null)
        {
            Console.Error.WriteLine("The provided certificate does not contain an ECC private key.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle for the signature field (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Add a signature field to the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            // Document.Form.Add expects a page number (1‑based)
            doc.Form.Add(sigField, 1);

            // Create a PKCS#7 signature object
            PKCS7 pkcs7 = new PKCS7
            {
                Reason = "Approved",
                Location = "Head Office",
                ContactInfo = "contact@example.com"
            };

            // CustomSignHash delegate: sign the hash with ECDSA (P‑256, SHA‑256)
            pkcs7.CustomSignHash = (byte[] hash, DigestHashAlgorithm alg) =>
                ecdsa.SignHash(hash); // DER‑encoded ECDSA signature

            // Sign the signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
