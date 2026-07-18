using System;
using System.IO;
using System.Linq;
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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create a signature field on the first page (adjust rectangle as needed)
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            // SignatureField constructor takes only page and rectangle; set the name via PartialName.
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "SmartCardSignature"
            };
            doc.Form.Add(sigField);

            // -----------------------------------------------------------------
            // 2. Obtain the X509Certificate2 from the smart card (example: first cert with a private key)
            // -----------------------------------------------------------------
            X509Certificate2 cert = GetCertificateFromSmartCard();
            if (cert == null)
            {
                Console.Error.WriteLine("No suitable certificate found on the smart card.");
                return;
            }

            // -----------------------------------------------------------------
            // 3. Create an ExternalSignature that uses the certificate.
            //    Set CustomSignHash delegate to perform the signing silently.
            // -----------------------------------------------------------------
            ExternalSignature externalSig = new ExternalSignature(cert)
            {
                Reason = "Document approved",
                Location = "Office",
                Date = DateTime.UtcNow
            };

            // CustomSignHash delegate: receives the hash bytes (and algorithm name) and returns the signature bytes.
            externalSig.CustomSignHash = (hash, algorithm) =>
            {
                // The algorithm argument is ignored because we know we are using SHA256.
                using (RSA rsa = cert.GetRSAPrivateKey())
                {
                    return rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            };

            // -----------------------------------------------------------------
            // 4. Sign the PDF using the signature field and the external signature.
            // -----------------------------------------------------------------
            sigField.Sign(externalSig);

            // -----------------------------------------------------------------
            // 5. Save the signed PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully: {outputPdf}");
    }

    // Helper method to retrieve a certificate with a private key from the smart card.
    // Adjust the selection criteria (e.g., subject name) as required.
    private static X509Certificate2 GetCertificateFromSmartCard()
    {
        // Open the personal (My) store of the current user.
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);

            // Find certificates that have a private key and are valid.
            var certs = store.Certificates
                .Find(X509FindType.FindByTimeValid, DateTime.Now, false)
                .Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false)
                .Cast<X509Certificate2>()
                .Where(c => c.HasPrivateKey);

            // Example: pick the first matching certificate.
            // In a real scenario, refine the selection (e.g., by subject name or thumbprint).
            return certs.FirstOrDefault();
        }
    }
}
