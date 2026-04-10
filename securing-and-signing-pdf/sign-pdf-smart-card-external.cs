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
        const string outputPdf = "signed.pdf";
        const string certThumbprint = "YOUR_CERT_THUMBPRINT";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the certificate from the smart‑card (CurrentUser\My store).
        X509Certificate2 cert = GetCertificateFromStore(certThumbprint);
        if (cert == null)
        {
            Console.Error.WriteLine("Certificate not found on smart card.");
            return;
        }

        // Create an ExternalSignature based on the smart‑card certificate.
        ExternalSignature extSignature = new ExternalSignature(cert);
        // Use a custom hash‑signing delegate that matches the required signature (hash, algorithm).
        extSignature.CustomSignHash = (hash, algorithm) =>
        {
            // The algorithm parameter is ignored because the certificate already defines the hash algorithm.
            using (RSA rsa = cert.GetRSAPrivateKey())
            {
                return rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        };

        using (Document doc = new Document(inputPdf))
        {
            // Define the signature field rectangle (coordinates in points).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page and add it to the form.
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            sigField.PartialName = "SmartCardSig";
            doc.Form.Add(sigField, 1); // page number is 1‑based

            // Sign the field with the external signature.
            sigField.Sign(extSignature);

            // Save the signed PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }

    // Retrieves a certificate with the specified thumbprint from the CurrentUser\My store.
    static X509Certificate2 GetCertificateFromStore(string thumbprint)
    {
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (var cert in store.Certificates)
            {
                if (string.Equals(cert.Thumbprint, thumbprint, StringComparison.OrdinalIgnoreCase))
                {
                    return cert;
                }
            }
        }
        return null;
    }
}
