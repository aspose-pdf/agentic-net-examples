using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class SmartCardPdfSigner
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string signatureFieldName = "Signature1"; // name of the signature field in the PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Retrieve the signature field by name (or by index if you prefer)
            SignatureField sigField = pdfDoc.Form[signatureFieldName] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
                return;
            }

            // Obtain the X509Certificate2 that resides on the smart card.
            // The certificate store on the smart card is exposed as a normal Windows store.
            // Adjust the search criteria (subject name, thumbprint, etc.) as needed.
            X509Certificate2 smartCardCert = GetCertificateFromSmartCard();
            if (smartCardCert == null)
            {
                Console.Error.WriteLine("Smart card certificate not found.");
                return;
            }

            // Create an ExternalSignature that uses the smart‑card certificate.
            // ExternalSignature works with certificates whose private key is non‑exportable
            // (typical for smart cards) and does not require a PFX file.
            ExternalSignature externalSig = new ExternalSignature(smartCardCert)
            {
                Reason      = "Document approved",
                Location    = "Head Office",
                ContactInfo = "security@example.com",
                // ShowProperties = false; // optional: hide default appearance text
            };

            // Sign the field. No PIN prompt will appear if the smart‑card middleware
            // has already cached the PIN or if the PIN is not required for this operation.
            sigField.Sign(externalSig);

            // Save the signed PDF (lifecycle rule: use the built‑in Save method)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }

    // Helper method to locate the certificate on the smart card.
    // This example searches the CurrentUser "My" store for a certificate that has a private key.
    // In a real scenario you may need to filter by issuer, thumbprint, or other criteria.
    private static X509Certificate2 GetCertificateFromSmartCard()
    {
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            // Find the first certificate that has a private key and is marked as "Smart Card"
            X509Certificate2 cert = store.Certificates
                .OfType<X509Certificate2>()
                .FirstOrDefault(c => c.HasPrivateKey && IsSmartCardCertificate(c));

            // The PIN prompt is handled by the CSP/PKCS#11 provider; if the PIN is cached,
            // the call below will not trigger a UI prompt.
            return cert;
        }
    }

    // Simple heuristic to identify a smart‑card certificate.
    // Adjust this logic according to your environment (e.g., check Issuer, Subject, or ProviderName).
    private static bool IsSmartCardCertificate(X509Certificate2 cert)
    {
        // Many smart‑card CSPs expose the provider name containing "SmartCard" or "KSP".
        // This check is optional and can be refined.
        string provider = cert.PrivateKey?.KeyExchangeAlgorithm ?? string.Empty;
        return provider.IndexOf("smartcard", StringComparison.OrdinalIgnoreCase) >= 0 ||
               provider.IndexOf("ksp", StringComparison.OrdinalIgnoreCase) >= 0;
    }
}